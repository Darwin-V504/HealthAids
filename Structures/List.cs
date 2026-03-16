namespace HealthAids.Structures
{
    public class Node<T>
    {
        public T Data { get; set; }
        public Node<T>? Next { get; set; }

        public Node(T data)
        {
            Data = data;
            Next = null;
        }
    }

    public class List<T>
    {
        private Node<T>? head;
        private int count;

        public List()
        {
            head = null;
            count = 0;
        }

        // Insertar al final
        public void Add(T data)
        {
            Node<T> newNode = new Node<T>(data);

            if (head == null)
            {
                head = newNode;
            }
            else
            {
                Node<T> current = head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newNode;
            }
            count++;
        }

        // Insertar al inicio
        public void AddFirst(T data)
        {
            Node<T> newNode = new Node<T>(data);
            newNode.Next = head;
            head = newNode;
            count++;
        }

        // Eliminar por condicion
        public bool Remove(Func<T, bool> predicate)
        {
            if (head == null) return false;

            if (predicate(head.Data))
            {
                head = head.Next;
                count--;
                return true;
            }

            Node<T> current = head;
            while (current.Next != null)
            {
                if (predicate(current.Next.Data))
                {
                    current.Next = current.Next.Next;
                    count--;
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        // Eliminar por índice
        public bool RemoveAt(int index)
        {
            if (index < 0 || index >= count) return false;

            if (index == 0)
            {
                head = head?.Next;
                count--;
                return true;
            }

            Node<T> current = head;
            for (int i = 0; i < index - 1; i++)
            {
                current = current!.Next;
            }

            current!.Next = current.Next?.Next;
            count--;
            return true;
        }

        // Buscar por condicion
        public T? Find(Func<T, bool> predicate)
        {
            Node<T>? current = head;
            while (current != null)
            {
                if (predicate(current.Data))
                    return current.Data;
                current = current.Next;
            }
            return default;
        }

        // Buscar todos por condicion
        public List<T> FindAll(Func<T, bool> predicate)
        {
            List<T> result = new();
            Node<T>? current = head;
            while (current != null)
            {
                if (predicate(current.Data))
                    result.Add(current.Data);
                current = current.Next;
            }
            return result;
        }

        
        public T? GetAt(int index)
        {
            if (index < 0 || index >= count) return default;

            Node<T>? current = head;
            for (int i = 0; i < index; i++)
            {
                current = current!.Next;
            }
            return current!.Data;
        }

        // Obtener todos
        public List<T> GetAll()
        {
            List<T> result = new();
            Node<T>? current = head;
            while (current != null)
            {
                result.Add(current.Data);
                current = current.Next;
            }
            return result;
        }

        // Actualizar por condicion
        public bool Update(Func<T, bool> predicate, T newData)
        {
            Node<T>? current = head;
            while (current != null)
            {
                if (predicate(current.Data))
                {
                    current.Data = newData;
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        // Verificar si existe
        public bool Exists(Func<T, bool> predicate)
        {
            Node<T>? current = head;
            while (current != null)
            {
                if (predicate(current.Data))
                    return true;
                current = current.Next;
            }
            return false;
        }

        // Limpiar la lista
        public void Clear()
        {
            head = null;
            count = 0;
        }

        // Obtener tamaño
        public int Count => count;

        // Verificar si está vacia
        public bool IsEmpty => head == null;
    }
}