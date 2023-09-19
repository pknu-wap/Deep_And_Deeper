using System;
using MBT;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace MBTEditor
{
    public class BehaviourTreeWindow : EditorWindow, IHasCustomMenu
    {
        private readonly Color _editorBackgroundColor = new(0.16f, 0.19f, 0.25f, 1);

        private readonly float _handleDetectionDistance = 8f;
        private GUIStyle _defaultNodeStyle;
        private GUIStyle _failureNodeStyle;
        private GUIStyle _lockButtonStyle;
        private GUIStyle _nodeBreakpointLabelStyle;
        private GUIStyle _nodeContentBoxStyle;
        private GUIStyle _nodeLabelStyle;
        private GUIStyle _runningNodeStyle;
        private GUIStyle _selectedNodeStyle;
        private GUIStyle _successNodeStyle;
        private NodeHandle currentHandle;
        private MonoBehaviourTree currentMBT;
        private Editor currentMBTEditor;
        private Node[] currentNodes;
        private NodeHandle dropdownHandleCache;
        private bool locked;
        private NodeDropdown nodeDropdown;
        private Vector2 nodeDropdownTargetPosition;

        private Rect nodeFinderActivatorRect;
        private bool nodeMoved;
        private Node selectedNode;
        private bool snapNodesToGrid;
        private Vector2 workspaceOffset;

        private void OnEnable()
        {
            // Read snap option
            snapNodesToGrid = EditorPrefs.GetBool("snapNodesToGrid", true);
            // Setup events
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
            Undo.undoRedoPerformed -= UpdateSelection;
            Undo.undoRedoPerformed += UpdateSelection;
            // Node finder
            nodeDropdown = new NodeDropdown(new AdvancedDropdownState(), AddNode);
            // Standard node
            _defaultNodeStyle = new GUIStyle
            {
                border = new RectOffset(10, 10, 10, 10),
                normal =
                {
                    background = Resources.Load("mbt_node_default", typeof(Texture2D)) as Texture2D
                }
            };
            // Selected node
            _selectedNodeStyle = new GUIStyle
            {
                border = new RectOffset(10, 10, 10, 10),
                normal =
                {
                    background = Resources.Load("mbt_node_selected", typeof(Texture2D)) as Texture2D
                }
            };
            // Success node
            _successNodeStyle = new GUIStyle
            {
                border = new RectOffset(10, 10, 10, 10),
                normal =
                {
                    background = Resources.Load("mbt_node_success", typeof(Texture2D)) as Texture2D
                }
            };
            // Failure node
            _failureNodeStyle = new GUIStyle
            {
                border = new RectOffset(10, 10, 10, 10),
                normal =
                {
                    background = Resources.Load("mbt_node_failure", typeof(Texture2D)) as Texture2D
                }
            };
            // Running node
            _runningNodeStyle = new GUIStyle
            {
                border = new RectOffset(10, 10, 10, 10),
                normal =
                {
                    background = Resources.Load("mbt_node_running", typeof(Texture2D)) as Texture2D
                }
            };
            // Node content box
            _nodeContentBoxStyle = new GUIStyle
            {
                padding = new RectOffset(0, 0, 15, 15)
            };
            // Node label
            _nodeLabelStyle = new GUIStyle
            {
                normal =
                {
                    textColor = Color.white
                },
                alignment = TextAnchor.MiddleCenter,
                wordWrap = true,
                margin = new RectOffset(10, 10, 10, 10),
                font = Resources.Load("mbt_Lato-Regular", typeof(Font)) as Font,
                fontSize = 12
            };
            // Node label when breakpoint is set to true
            _nodeBreakpointLabelStyle = new GUIStyle(_nodeLabelStyle)
            {
                normal =
                {
                    textColor = new Color(1f, 0.35f, 0.18f)
                }
            };
        }

        private void OnDisable()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            Undo.undoRedoPerformed -= UpdateSelection;
        }

        private void OnGUI()
        {
            // Draw grid in background first
            PaintBackground();

            // If there is no tree to render just skip rest
            if (currentMBT == null)
            {
                // Keep toolbar rendered
                PaintWindowToolbar();
                return;
            }

            PaintConnections(Event.current);

            // Repaint nodes
            PaintNodes();

            PaintWindowToolbar();

            // Update selection and drag
            ProcessEvents(Event.current);

            if (GUI.changed) Repaint();
        }

        private void OnFocus()
        {
            UpdateSelection();
            Repaint();
        }

        private void OnInspectorUpdate()
        {
            // OPTIMIZE: This can be optimized to call repaint once per second
            Repaint();
        }

        private void OnSelectionChange()
        {
            var previous = currentMBT;
            UpdateSelection();
            // Reset workspace position only when selection changed
            if (previous != currentMBT) workspaceOffset = Vector2.zero;

            Repaint();
        }

        // http://leahayes.co.uk/2013/04/30/adding-the-little-padlock-button-to-your-editorwindow.html
        private void ShowButton(Rect pos)
        {
            // Cache style
            _lockButtonStyle ??= "IN LockButton";
            // Generic menu button
            GUI.enabled = currentMBT != null;
            locked = GUI.Toggle(pos, locked, GUIContent.none, _lockButtonStyle);
            GUI.enabled = true;
        }

        void IHasCustomMenu.AddItemsToMenu(GenericMenu menu)
        {
            menu.AddItem(new GUIContent("Lock"), locked, () =>
            {
                locked = !locked;
                UpdateSelection();
            });
        }

        [MenuItem("Window/Mono Behaviour Tree")]
        public static void OpenEditor()
        {
            var window = GetWindow<BehaviourTreeWindow>();
            window.titleContent = new GUIContent(
                "Behaviour Tree",
                Resources.Load("mbt_window_icon", typeof(Texture2D)) as Texture2D
            );
        }

        private void PaintConnections(Event e)
        {
            // Paint line when dragging connection
            if (currentHandle != null)
            {
                Handles.BeginGUI();
                var p1 = new Vector3(currentHandle.position.x, currentHandle.position.y, 0f);
                var p2 = new Vector3(e.mousePosition.x, e.mousePosition.y, 0f);
                Handles.DrawBezier(p1, p2, p1, p2, new Color(0.3f, 0.36f, 0.5f), null, 4f);
                Handles.EndGUI();
            }

            // Paint all current connections
            foreach (var n in currentNodes)
            {
                Vector3 p1 = GetBottomHandlePosition(n.rect) + workspaceOffset;
                foreach (var t in n.children)
                {
                    Handles.BeginGUI();
                    Vector3 p2 = GetTopHandlePosition(t.rect) + workspaceOffset;
                    Handles.DrawBezier(p1, p2, p1, p2, new Color(0.3f, 0.36f, 0.5f), null, 4f);
                    Handles.EndGUI();
                }
            }
        }

        private void PaintWindowToolbar()
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
            EditorGUI.BeginDisabledGroup(currentMBT == null);
            if (GUILayout.Toggle(snapNodesToGrid, "Snap Nodes", EditorStyles.toolbarButton) != snapNodesToGrid)
            {
                snapNodesToGrid = !snapNodesToGrid;
                // Store this setting
                EditorPrefs.SetBool("snapNodesToGrid", snapNodesToGrid);
            }

            // TODO: AutoLayout
            // if (GUILayout.Button("Auto Layout", EditorStyles.toolbarButton)){
            //     Debug.Log("Auto layout is not implemented.");
            // }
            EditorGUILayout.Space();
            if (GUILayout.Button("Focus Root", EditorStyles.toolbarButton)) FocusRoot();

            if (GUILayout.Button("Select In Hierarchy", EditorStyles.toolbarButton))
                if (currentMBT != null)
                {
                    var gameObject = currentMBT.gameObject;
                    Selection.activeGameObject = gameObject;
                    EditorGUIUtility.PingObject(gameObject);
                }

            if (GUILayout.Button("Add Node", EditorStyles.toolbarDropDown))
                OpenNodeFinder(nodeFinderActivatorRect, false);

            if (Event.current.type == EventType.Repaint) nodeFinderActivatorRect = GUILayoutUtility.GetLastRect();
            EditorGUI.EndDisabledGroup();
            GUILayout.FlexibleSpace();
            if (currentMBT != null) GUILayout.Label($"{currentMBT.name} {-workspaceOffset}");

            EditorGUILayout.EndHorizontal();
        }

        private void FocusRoot()
        {
            Root rootNode = null;
            foreach (var t in currentNodes)
            {
                if (t is not Root root) continue;
                rootNode = root;
                break;
            }

            if (rootNode != null)
                workspaceOffset = -rootNode.rect.center + new Vector2(position.width / 2, position.height / 2);
        }

        private void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            // Disable lock when changing state
            locked = false;
            UpdateSelection();
            Repaint();
        }

        // DeselectNode cannot be called here
        // void OnLostFocus()
        // {
        //     // DeselectNode();
        // }

        private void UpdateSelection()
        {
            var prevMBT = currentMBT;
            if (!locked && Selection.activeGameObject != null)
            {
                currentMBT = Selection.activeGameObject.GetComponent<MonoBehaviourTree>();
                // If new selection is null then restore previous one
                if (currentMBT == null) currentMBT = prevMBT;
            }

            if (currentMBT != prevMBT)
                // Get new editor for new MBT
                Editor.CreateCachedEditor(currentMBT, null, ref currentMBTEditor);

            if (currentMBT != null)
            {
                currentNodes = currentMBT.GetComponents<Node>();
                // Ensure there is no error when node script is missing
                foreach (var t in currentNodes) t.children.RemoveAll(item => item == null);
            }
            else
            {
                currentNodes = Array.Empty<Node>();
                // Unlock when there is nothing to display
                locked = false;
            }
        }

        private void ProcessEvents(Event e)
        {
            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 0)
                    {
                        // Reset flag
                        nodeMoved = false;
                        // First check if any node handle was clicked
                        var handle = FindHandle(e.mousePosition);
                        if (handle != null)
                        {
                            currentHandle = handle;
                            e.Use();
                            break;
                        }

                        var node = FindNode(e.mousePosition);
                        // Select node if contains point
                        if (node != null)
                        {
                            DeselectNode();
                            SelectNode(node);
                            if (e.clickCount == 2 && node is SubTree subTree)
                                if (subTree.tree != null)
                                    Selection.activeGameObject = subTree.tree.gameObject;
                        }
                        else
                        {
                            DeselectNode();
                        }

                        e.Use();
                    }
                    else if (e.button == 1)
                    {
                        var node = FindNode(e.mousePosition);
                        // Open proper context menu
                        if (node != null)
                        {
                            OpenNodeMenu(e.mousePosition, node);
                        }
                        else
                        {
                            DeselectNode();
                            OpenNodeFinder(new Rect(e.mousePosition.x, e.mousePosition.y, 1, 1));
                        }

                        e.Use();
                    }

                    break;
                case EventType.MouseDrag:
                    // Drag node, workspace or connection
                    if (e.button == 0)
                    {
                        if (currentHandle != null)
                        {
                            // Let PaintConnections draw lines
                        }
                        else if (selectedNode != null)
                        {
                            Undo.RecordObject(selectedNode, "Move Node");
                            selectedNode.rect.position += Event.current.delta;
                            // Move whole branch when Ctrl is pressed
                            if (e.control)
                            {
                                var movedNodes = selectedNode.GetAllSuccessors();
                                foreach (var t in movedNodes)
                                {
                                    Undo.RecordObject(t, "Move Node");
                                    t.rect.position += Event.current.delta;
                                }
                            }

                            nodeMoved = true;
                        }
                        else
                        {
                            workspaceOffset += Event.current.delta;
                        }

                        GUI.changed = true;
                        e.Use();
                    }

                    break;
                case EventType.MouseUp:
                    if (currentHandle != null) TryConnectNodes(currentHandle, e.mousePosition);

                    // Reorder or snap nodes in case any of them was moved
                    if (nodeMoved && selectedNode != null)
                    {
                        // Snap nodes if option is enabled
                        if (snapNodesToGrid)
                        {
                            Undo.RecordObject(selectedNode, "Move Node");
                            selectedNode.rect.position = SnapPositionToGrid(selectedNode.rect.position);
                            // When control is pressed snap successors too
                            if (e.control)
                            {
                                var movedNodes = selectedNode.GetAllSuccessors();
                                foreach (var t in movedNodes)
                                {
                                    Undo.RecordObject(t, "Move Node");
                                    t.rect.position = SnapPositionToGrid(t.rect.position);
                                }
                            }
                        }

                        // Reorder siblings if selected node has parent
                        if (selectedNode.parent != null)
                        {
                            Undo.RecordObject(selectedNode.parent, "Move Node");
                            selectedNode.parent.SortChildren();
                        }
                    }

                    nodeMoved = false;
                    currentHandle = null;
                    GUI.changed = true;
                    break;
            }
        }

        private static Vector2 SnapPositionToGrid(Vector2 position)
        {
            return new Vector2(
                Mathf.Round(position.x / 20f) * 20f,
                Mathf.Round(position.y / 20f) * 20f
            );
        }

        private void TryConnectNodes(NodeHandle handle, Vector2 mousePosition)
        {
            // Find hovered node and connect or open dropdown
            var targetNode = FindNode(mousePosition);
            if (targetNode == null)
            {
                OpenNodeFinder(new Rect(mousePosition.x, mousePosition.y, 1, 1), true, handle);
                return;
            }

            // Check if they are not the same node
            if (targetNode == handle.node) return;

            Undo.RecordObject(targetNode, "Connect Nodes");
            Undo.RecordObject(handle.node, "Connect Nodes");
            switch (handle.type)
            {
                // There is node, try to connect if this is possible
                case HandleType.Input when targetNode is IParentNode:
                {
                    // Do not allow connecting descendants as parents
                    if (targetNode.IsDescendantOf(handle.node)) return;

                    // Then add as child to new parent
                    targetNode.AddChild(handle.node);
                    // Update order of nodes
                    targetNode.SortChildren();
                    break;
                }
                case HandleType.Output when targetNode is IChildrenNode:
                {
                    // Do not allow connecting descendants as parents
                    if (handle.node.IsDescendantOf(targetNode)) return;

                    // Then add as child to new parent
                    handle.node.AddChild(targetNode);
                    // Update order of nodes
                    handle.node.SortChildren();
                    break;
                }
            }
        }

        private void SelectNode(Node node)
        {
            currentMBT.selectedEditorNode = node;
            currentMBTEditor.Repaint();
            node.selected = true;
            selectedNode = node;
            GUI.changed = true;
        }

        private void DeselectNode(Node node)
        {
            currentMBT.selectedEditorNode = null;
            currentMBTEditor.Repaint();
            node.selected = false;
            selectedNode = null;
            GUI.changed = true;
        }

        private void DeselectNode()
        {
            currentMBT.selectedEditorNode = null;
            currentMBTEditor.Repaint();
            foreach (var t in currentNodes) t.selected = false;

            selectedNode = null;
            GUI.changed = true;
        }

        private Node FindNode(Vector2 mousePosition)
        {
            foreach (var t in currentNodes)
            {
                // Get correct position of node with offset
                var target = t.rect;
                target.position += workspaceOffset;
                if (target.Contains(mousePosition)) return t;
            }

            return null;
        }

        private NodeHandle FindHandle(Vector2 mousePosition)
        {
            foreach (var node in currentNodes)
            {
                // Get correct position of node with offset
                var targetRect = node.rect;
                targetRect.position += workspaceOffset;

                if (node is IChildrenNode)
                {
                    var handlePoint = GetTopHandlePosition(targetRect);
                    if (Vector2.Distance(handlePoint, mousePosition) < _handleDetectionDistance)
                        return new NodeHandle(node, handlePoint, HandleType.Input);
                }

                if (node is not IParentNode) continue;
                {
                    var handlePoint = GetBottomHandlePosition(targetRect);
                    if (Vector2.Distance(handlePoint, mousePosition) < _handleDetectionDistance)
                        return new NodeHandle(node, handlePoint, HandleType.Output);
                }
            }

            return null;
        }

        private void PaintNodes()
        {
            for (var i = currentNodes.Length - 1; i >= 0; i--)
            {
                var node = currentNodes[i];
                var targetRect = node.rect;
                targetRect.position += workspaceOffset;
                // Draw node content
                GUILayout.BeginArea(targetRect, GetNodeStyle(node));
                GUILayout.BeginVertical(_nodeContentBoxStyle);
                GUILayout.Label(node.title, node.breakpoint ? _nodeBreakpointLabelStyle : _nodeLabelStyle);
                GUILayout.EndVertical();
                if (Event.current.type == EventType.Repaint) node.rect.height = GUILayoutUtility.GetLastRect().height;

                GUILayout.EndArea();

                // Paint warning icon
                if (!Application.isPlaying && !node.IsValid())
                    GUI.Label(GetWarningIconRect(targetRect), EditorGUIUtility.IconContent("CollabConflict Icon"));

                // Draw connection handles if needed
                if (node is IChildrenNode)
                {
                    var top = GetTopHandlePosition(targetRect);
                    GUI.DrawTexture(
                        new Rect(top.x - 8, top.y - 5, 16, 16),
                        Resources.Load("mbt_node_handle", typeof(Texture2D)) as Texture2D
                    );
                }

                if (node is not IParentNode) continue;
                var bottom = GetBottomHandlePosition(targetRect);
                GUI.DrawTexture(
                    new Rect(bottom.x - 8, bottom.y - 11, 16, 16),
                    Resources.Load("mbt_node_handle", typeof(Texture2D)) as Texture2D
                );
            }
        }

        private GUIStyle GetNodeStyle(Node node)
        {
            if (node.selected) return _selectedNodeStyle;

            return node.status switch
            {
                Status.Success => _successNodeStyle,
                Status.Failure => _failureNodeStyle,
                Status.Running => _runningNodeStyle,
                _ => _defaultNodeStyle
            };
        }

        private static Vector2 GetTopHandlePosition(Rect rect)
        {
            return new Vector2(rect.x + rect.width / 2, rect.y);
        }

        private static Vector2 GetBottomHandlePosition(Rect rect)
        {
            return new Vector2(rect.x + rect.width / 2, rect.y + rect.height);
        }

        private static Rect GetWarningIconRect(Rect rect)
        {
            // return new Rect(rect.x - 10, rect.y + rect.height/2 - 10 , 20, 20);
            return new Rect(rect.x + rect.width - 2, rect.y - 1, 20, 20);
        }

        private void OpenNodeFinder(Rect rect, bool useRectPosition = true, NodeHandle handle = null)
        {
            // Store handle to connect later (null by default)
            dropdownHandleCache = handle;
            // Store real clicked position including workspace offset
            if (useRectPosition)
                nodeDropdownTargetPosition = rect.position - workspaceOffset;
            else
                nodeDropdownTargetPosition = new Vector2(position.width / 2, position.height / 2) - workspaceOffset;

            // Open dropdown
            nodeDropdown.Show(rect);
        }

        private void OpenNodeMenu(Vector2 mousePosition, Node node)
        {
            var genericMenu = new GenericMenu();
            genericMenu.AddItem(new GUIContent("Breakpoint"), node.breakpoint, () => ToggleNodeBreakpoint(node));
            genericMenu.AddItem(new GUIContent("Duplicate"), false, () => DuplicateNode(node));
            genericMenu.AddItem(new GUIContent("Disconnect Children"), false, () => DisconnectNodeChildren(node));
            genericMenu.AddItem(new GUIContent("Disconnect Parent"), false, () => DisconnectNodeParent(node));
            genericMenu.AddItem(new GUIContent("Delete Node"), false, () => DeleteNode(node));
            genericMenu.ShowAsContext();
        }

        private void AddNode(ClassTypeDropdownItem item)
        {
            // In case there is nothing to add
            if (currentMBT == null || item.classType == null) return;

            // Allow only one root
            if (item.classType.IsAssignableFrom(typeof(Root)) && currentMBT.GetComponent<Root>() != null)
            {
                Debug.LogWarning("You can not add more than one Root node.");
                return;
            }

            Undo.SetCurrentGroupName("Create Node");
            var node = (Node)Undo.AddComponent(currentMBT.gameObject, item.classType);
            node.title = item.name;
            node.hideFlags = HideFlags.HideInInspector;
            node.rect.position = nodeDropdownTargetPosition - new Vector2(node.rect.width / 2, 0);
            UpdateSelection();
            if (dropdownHandleCache != null)
                // Add additional offset (3,3) to be sure that point is inside rect
                TryConnectNodes(dropdownHandleCache, nodeDropdownTargetPosition + workspaceOffset + new Vector2(3, 3));
        }

        private static void ToggleNodeBreakpoint(Node node)
        {
            // Toggle breakpoint flag
            // Undo.RecordObject(node, "Toggle Breakpoint");
            node.breakpoint = !node.breakpoint;
        }

        private void DeleteNode(Node node)
        {
            if (currentMBT == null) return;

            DeselectNode();
            // Disconnect all children and parent
            Undo.SetCurrentGroupName("Delete Node");
            DisconnectNodeChildren(node);
            DisconnectNodeParent(node);
            Undo.DestroyObjectImmediate(node);
            // DestroyImmediate(node, true);
            UpdateSelection();
        }

        private static void DisconnectNodeParent(Node node)
        {
            if (node.parent == null) return;
            Undo.RecordObject(node, "Disconnect Parent");
            Undo.RecordObject(node.parent, "Disconnect Parent");
            node.parent.RemoveChild(node);
        }

        private static void DisconnectNodeChildren(Node node)
        {
            Undo.RecordObject(node, "Disconnect Children");
            for (var i = node.children.Count - 1; i >= 0; i--)
            {
                Undo.RecordObject(node.children[i], "Disconnect Children");
                node.RemoveChild(node.children[i]);
            }
        }

        private void DuplicateNode(Node contextNode)
        {
            // NOTE: This code is mostly copied from AddNode()
            // Check if there is MBT
            if (currentMBT == null) return;

            var classType = contextNode.GetType();
            // Allow only one root
            if (classType.IsAssignableFrom(typeof(Root)) && currentMBT.GetComponent<Root>() != null)
            {
                Debug.LogWarning("You can not add more than one Root node.");
                return;
            }

            Undo.SetCurrentGroupName("Duplicate Node");
            var node = (Node)Undo.AddComponent(currentMBT.gameObject, classType);
            // Copy values
            EditorUtility.CopySerialized(contextNode, node);
            // Set flags anyway to ensure it is not visible in inspector
            node.hideFlags = HideFlags.HideInInspector;
            node.rect.position = contextNode.rect.position + new Vector2(20, 20);
            // Remove all connections or graph gonna break
            node.parent = null;
            node.children.Clear();
            UpdateSelection();
        }

        /// It is quite unique, but https://stackoverflow.com/questions/2920696/how-generate-unique-integers-based-on-guids
        private int GenerateId()
        {
            return Guid.NewGuid().GetHashCode();
        }

        private void PaintBackground()
        {
            // Background
            Handles.BeginGUI();
            Handles.DrawSolidRectangleWithOutline(new Rect(0, 0, position.width, position.height),
                _editorBackgroundColor, Color.gray);
            Handles.EndGUI();
            // Grid lines
            DrawBackgroundGrid(20, 0.1f, new Color(0.3f, 0.36f, 0.5f));
            DrawBackgroundGrid(100, 0.2f, new Color(0.3f, 0.36f, 0.5f));
        }

        /// Method copied from https://gram.gs/gramlog/creating-node-based-editor-unity/
        private void DrawBackgroundGrid(float gridSpacing, float gridOpacity, Color gridColor)
        {
            var widthDivs = Mathf.CeilToInt(position.width / gridSpacing);
            var heightDivs = Mathf.CeilToInt(position.height / gridSpacing);

            Handles.BeginGUI();

            Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

            var newOffset = new Vector3(workspaceOffset.x % gridSpacing, workspaceOffset.y % gridSpacing, 0);

            for (var i = 0; i <= widthDivs; i++)
                Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset,
                    new Vector3(gridSpacing * i, position.height + gridSpacing, 0f) + newOffset);

            for (var j = 0; j <= heightDivs; j++)
                Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * j, 0) + newOffset,
                    new Vector3(position.width + gridSpacing, gridSpacing * j, 0f) + newOffset);

            Handles.color = Color.white;
            Handles.EndGUI();
        }

        private class NodeHandle
        {
            public readonly Node node;
            public readonly Vector2 position;
            public readonly HandleType type;

            public NodeHandle(Node node, Vector2 position, HandleType type)
            {
                this.node = node;
                this.position = position;
                this.type = type;
            }
        }

        private enum HandleType
        {
            Input,
            Output
        }
    }
}