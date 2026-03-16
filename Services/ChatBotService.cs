// Services/ChatbotService.cs
using HealthAids.Models;
using HealthAids.Structures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthAids.Services
{
    public class ChatbotService
    {
        private readonly BinaryTree<ChatbotNode> _chatbotTree;
        private ChatbotNode? _currentNode;

        public ChatbotService()
        {
            _chatbotTree = new BinaryTree<ChatbotNode>();
            InitializeChatbot();
            _currentNode = GetNodeById(1); // Empezar en nodo raíz
        }

        private void InitializeChatbot()
        {
            // Crear nodos del arbol de decisión - Usando List<ChatbotNode> de System.Collections.Generic
            var nodes = new System.Collections.Generic.List<ChatbotNode>
            {
                new ChatbotNode
                {
                    Id = 1,
                    Message = " Hola Soy el asistente virtual de HealthAids. ¿En qué puedo ayudarte hoy?",
                    Options = new System.Collections.Generic.List<ChatbotOption>
                    {
                        new ChatbotOption { Id = 1, Text = "Agendar una cita médica", NextNodeId = 2 },
                        new ChatbotOption { Id = 2, Text = "Ver doctores disponibles", NextNodeId = 3 },
                        new ChatbotOption { Id = 3, Text = "Información sobre HealthAids", NextNodeId = 4 },
                        new ChatbotOption { Id = 4, Text = "Contacto y soporte", NextNodeId = 5 }
                    }
                },
                new ChatbotNode
                {
                    Id = 2,
                    Message = "¿Para qué especialidad médica deseas agendar tu cita?",
                    Options = new System.Collections.Generic.List<ChatbotOption>
                    {
                        new ChatbotOption { Id = 5, Text = "Cardiología", NextNodeId = 6 },
                        new ChatbotOption { Id = 6, Text = "Pediatría", NextNodeId = 6 },
                        new ChatbotOption { Id = 7, Text = "Odontología", NextNodeId = 6 },
                        new ChatbotOption { Id = 8, Text = "Neurología", NextNodeId = 6 },
                        new ChatbotOption { Id = 9, Text = "Volver al inicio", NextNodeId = 1 }
                    }
                },
                new ChatbotNode
                {
                    Id = 3,
                    Message = " Estos son nuestros especialistas disponibles:\n\n" +
                              "Dr. Juan Pérez - Cardiología\n" +
                              "Lunes a Viernes 9am - 2pm\n\n" +
                              "Dra. María García** - Pediatría\n" +
                              "Lunes a Viernes 10am - 4pm\n\n" +
                              "Dr. Carlos López** - Odontología\n" +
                              "Martes y Jueves 3pm - 7pm\n\n" +
                              "Dra. Ana Martínez - Neurología\n" +
                              "Miércoles 8am - 12pm\n\n" +
                              "¿Deseas agendar una cita con alguno de ellos?",
                    Options = new System.Collections.Generic.List<ChatbotOption>
                    {
                        new ChatbotOption { Id = 10, Text = "Sí, quiero agendar", NextNodeId = 2 },
                        new ChatbotOption { Id = 11, Text = "Volver al inicio", NextNodeId = 1 }
                    }
                },
                new ChatbotNode
                {
                    Id = 4,
                    Message = "HealthAids es una plataforma de gestión hospitalaria que te permite:\n\n" +
                              "Agendar citas médicas de forma rápida y sencilla\n" +
                              "Consultar tu historial de citas pasadas\n" +
                              "Recibir recordatorios automáticos de tus citas\n" +
                              "Chat de asistencia 24/7 para resolver tus dudas\n\n" +
                              "Nuestro objetivo es facilitar el acceso a la salud de manera digital y eficiente.",
                    Options = new System.Collections.Generic.List<ChatbotOption>
                    {
                        new ChatbotOption { Id = 12, Text = " Quiero agendar una cita", NextNodeId = 2 },
                        new ChatbotOption { Id = 13, Text = " Ver doctores", NextNodeId = 3 },
                        new ChatbotOption { Id = 14, Text = " Volver al inicio", NextNodeId = 1 }
                    }
                },
                new ChatbotNode
                {
                    Id = 5,
                    Message = "Puedes contactarnos a través de:\n\n" +
                              "Email:soporte@healthaids.com\n" +
                              "Horario de atención:**\n" +
                              "Lunes a Viernes: 8:00 am - 6:00 pm\n" +
                              "Sábados: 9:00 am - 1:00 pm\n\n" +
                              "¿Hay algo más en que pueda ayudarte?",
                    Options = new System.Collections.Generic.List<ChatbotOption>
                    {
                        new ChatbotOption { Id = 15, Text = " Agendar cita", NextNodeId = 2 },
                        new ChatbotOption { Id = 16, Text = " Volver al inicio", NextNodeId = 1 }
                    }
                },
                new ChatbotNode
                {
                    Id = 6,
                    Message = "Para agendar una cita, necesitas iniciar sesión o registrarte.\n\n" +
                              "Si ya tienes una cuenta, inicia sesión para continuar.\n" +
                              "Si eres nuevo, regístrate en segundos.",
                    Options = new System.Collections.Generic.List<ChatbotOption>
                    {
                        new ChatbotOption { Id = 17, Text = "Iniciar sesión", NextNodeId = 1, Action = "login" },
                        new ChatbotOption { Id = 18, Text = "Registrarme", NextNodeId = 1, Action = "register" },
                        new ChatbotOption { Id = 19, Text = "Volver al inicio", NextNodeId = 1 }
                    },
                    IsEndNode = true
                },
               new ChatbotNode
               {
                  Id = 7,
                  Message = "¡Perfecto! Ya tienes una sesión activa. Puedes agendar tu cita desde la pantalla de citas.",
                  Options = new System.Collections.Generic.List<ChatbotOption>
                        {
                            
                         new ChatbotOption { Id = 20, Text = "Ir a mis citas", NextNodeId = 1, Action = "schedule" },
                         new ChatbotOption { Id = 21, Text = "Volver al inicio", NextNodeId = 1 }
                        }
                    },
                new ChatbotNode
                {
                    Id = 8,
                    Message = "Ya has iniciado sesión Quieres agendar tu cita ahora?",
                    Options = new System.Collections.Generic.List<ChatbotOption>
                    {
                        new ChatbotOption { Id = 22, Text = "Sí, agendar cita", NextNodeId = 1, Action = "schedule" },
                        new ChatbotOption { Id = 23, Text = "No, volver al inicio", NextNodeId = 1 }
                    }
                }

            };

            // Insertar nodos en el árbol (ordenados por Id)
            foreach (var node in nodes.OrderBy(n => n.Id))
            {
                _chatbotTree.Insert(node);
            }
        }

        private ChatbotNode? GetNodeById(int id)
        {
            
            var allNodes = _chatbotTree.InOrderTraversal();
            for (int i = 0; i < allNodes.Count; i++)
            {
                var node = allNodes.GetAt(i);
                if (node != null && node.Id == id)
                    return node;
            }
            return null;
        }

        public ChatbotNode? GetCurrentNode()
        {
            return _currentNode;
        }

        public ChatbotNode? SelectOption(int optionId, bool isAuthenticated = false)
        {
            if (_currentNode == null) return null;

            // Buscar la opcion seleccionada
            ChatbotOption? selectedOption = null;
            foreach (var option in _currentNode.Options)
            {
                if (option.Id == optionId)
                {
                    selectedOption = option;
                    break;
                }
            }

            if (selectedOption == null) return null;

           

            // caso 1: Si estamos en el nodo de especialidades (nodo 2) y el usuario esta autenticado
            if (_currentNode.Id == 2 && isAuthenticated)
            {
                

                // Crear un nodo especial para usuarios autenticados
                _currentNode = new ChatbotNode
                {
                    Id = 7,
                    Message = "¡Perfecto! Ya tienes una sesión activa. Puedes agendar tu cita desde la pantalla de citas.",
                    Options = new System.Collections.Generic.List<ChatbotOption>
            {
                new ChatbotOption { Id = 20, Text = "Ir a mis citas", NextNodeId = 1, Action = "schedule" },
                new ChatbotOption { Id = 21, Text = "Volver al inicio", NextNodeId = 1 }
            }
                };
                return _currentNode;
            }

            // caso 2: Si la opcion lleva al nodo 6 (login) y el usuario esta autenticado
            if (selectedOption.NextNodeId == 6 && isAuthenticated)
            {
                

                _currentNode = new ChatbotNode
                {
                    Id = 8,
                    Message = "Ya has iniciado sesión. ¿Quieres agendar tu cita ahora?",
                    Options = new System.Collections.Generic.List<ChatbotOption>
            {
                new ChatbotOption { Id = 22, Text = "Sí, agendar cita", NextNodeId = 1, Action = "schedule" },
                new ChatbotOption { Id = 23, Text = "No, volver al inicio", NextNodeId = 1 }
            }
                };
                return _currentNode;
            }

            // caso por defaultL: Ir al siguiente nodo
           
            _currentNode = GetNodeById(selectedOption.NextNodeId);
            return _currentNode;
        }

        public void Reset()
        {
            _currentNode = GetNodeById(1);
        }

        public ChatbotNode? GoToNode(int nodeId)
        {
            _currentNode = GetNodeById(nodeId);
            return _currentNode;
        }

        // Obtener todos los nodos
        public HealthAids.Structures.List<ChatbotNode> GetAllNodes()
        {
            return _chatbotTree.InOrderTraversal();
        }

        // Si necesitas una lista de System.Collections.Generic.List<ChatbotNode> en algun lugar
        public System.Collections.Generic.List<ChatbotNode> GetAllNodesAsSystemList()
        {
            var customList = _chatbotTree.InOrderTraversal();
            var sysList = new System.Collections.Generic.List<ChatbotNode>();
            for (int i = 0; i < customList.Count; i++)
            {
                var node = customList.GetAt(i);
                if (node != null)
                    sysList.Add(node);
            }
            return sysList;
        }

        // Obtener altura del arbol 
        public int GetTreeHeight()
        {
            return _chatbotTree.GetHeight();
        }

        // Obtener opciones del nodo actual
        public System.Collections.Generic.List<ChatbotOption>? GetCurrentOptions()
        {
            return _currentNode?.Options;
        }

        // Verificar si el nodo actual es terminal
        public bool IsCurrentNodeEnd()
        {
            return _currentNode?.IsEndNode ?? false;
        }

        // Obtener accion del nodo actual (si tiene)
        public string? GetCurrentAction()
        {
            return _currentNode?.Action;
        }
    }
}