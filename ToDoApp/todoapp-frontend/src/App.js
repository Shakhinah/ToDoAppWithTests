import React, { useState, useEffect } from 'react';
import axios from 'axios';

function App() {
    const [todos, setTodos] = useState([]); // State to store the list of todos
    const [content, setContent] = useState(''); // State to store the input value
    const [editId, setEditId] = useState(null); // State to track the todo being edited
    const [error, setError] = useState(''); // State to store error messages
    const [filter, setFilter] = useState('all'); // State to manage filter (all, completed, incomplete)
    const API_URL = 'http://localhost:5081/api/ToDo'; // Backend API URL

    // Fetch todos from the backend
    const fetchTodos = async () => {
        try {
            const response = await axios.get(API_URL);
            setTodos(response.data); // Update the todos state with the fetched data
            setError(''); // Clear any previous errors
        } catch (error) {
            setError("Failed to fetch todos. Please try again."); // Show error message
            console.error("Fetch error:", error.response?.data); // Log the error response
        }
    };

    // Handle form submission (add or update a todo)
    const handleSubmit = async (e) => {
        e.preventDefault();
        if (!content.trim()) {
            setError("Content cannot be empty!");
            return;
        }

        try {
            if (editId) {
                // Update an existing todo's content
                await axios.put(`${API_URL}/${editId}`, {
                    id: editId, // Include the ID in the request body
                    content: content, // Include the content
                });
            } else {
                // Add a new todo (do not include id in the payload)
                await axios.post(API_URL, { content: content });
            }

            setContent('');
            setEditId(null);
            setError('');
            fetchTodos();
        } catch (error) {
            setError(error.response?.data || "Failed to save todo. Please try again.");
            console.error("Submit error:", error.response?.data);
        }
    };

    // Handle deleting a todo
    const deleteTodo = async (id) => {
        try {
            await axios.delete(`${API_URL}/${id}`);
            setError(''); // Clear any previous errors
            fetchTodos(); // Refresh the todo list
        } catch (error) {
            setError("Failed to delete todo. Please try again."); // Show error message
            console.error("Delete error:", error.response?.data); // Log the error response
        }
    };

    // Handle editing a todo's content
    const editTodo = (todo) => {
        setContent(todo.content); // Set the input field to the todo's content
        setEditId(todo.id); // Set the edit ID to the todo's ID
    };

    // Toggle completion status of a todo
    const toggleCompletion = async (id) => {
        try {
            await axios.patch(`${API_URL}/${id}/toggle`);
            fetchTodos(); // Refresh the todo list
        } catch (error) {
            setError("Failed to update todo status. Please try again.");
            console.error("Toggle error:", error.response?.data); // Log the error response
        }
    };

    // Mark all todos as complete
    const markAllAsComplete = async () => {
        try {
            await axios.patch(`${API_URL}/mark-all-complete`);
            fetchTodos(); // Refresh the todo list
        } catch (error) {
            setError("Failed to mark all todos as complete. Please try again.");
            console.error("Mark all error:", error.response?.data); // Log the error response
        }
    };

    // Filter todos based on the selected filter
    const filteredTodos = todos.filter(todo => {
        if (filter === 'completed') return todo.isCompleted;
        if (filter === 'incomplete') return !todo.isCompleted;
        return true; // Show all todos
    });

    // Fetch todos when the component mounts
    useEffect(() => {
        fetchTodos();
    }, []);

    return (
        <div style={{ maxWidth: '600px', margin: '0 auto', padding: '20px', fontFamily: 'Arial, sans-serif' }}>
            <h1 style={{ textAlign: 'center', color: '#333' }}>Todo List</h1>
            {error && <p style={{ color: 'red', textAlign: 'center' }}>{error}</p>} {/* Display error message */}
            <form onSubmit={handleSubmit} style={{ display: 'flex', marginBottom: '20px' }}>
                <input
                    type="text"
                    value={content}
                    onChange={(e) => setContent(e.target.value)}
                    placeholder="Add new todo"
                    style={{ flex: 1, padding: '8px', fontSize: '16px', border: '1px solid #ccc', borderRadius: '4px' }}
                />
                <button
                    type="submit"
                    style={{
                        padding: '8px 16px',
                        marginLeft: '10px',
                        backgroundColor: '#28a745',
                        color: '#fff',
                        border: 'none',
                        borderRadius: '4px',
                        cursor: 'pointer',
                    }}
                >
                    {editId ? 'Update' : 'Add'}
                </button>
            </form>

            <div style={{ marginBottom: '20px' }}>
                <button
                    onClick={markAllAsComplete}
                    style={{
                        padding: '8px 16px',
                        backgroundColor: '#007bff',
                        color: '#fff',
                        border: 'none',
                        borderRadius: '4px',
                        cursor: 'pointer',
                        marginRight: '10px',
                    }}
                >
                    Mark All as Complete
                </button>
                <select
                    value={filter}
                    onChange={(e) => setFilter(e.target.value)}
                    style={{ padding: '8px', fontSize: '16px', borderRadius: '4px', border: '1px solid #ccc' }}
                >
                    <option value="all">All</option>
                    <option value="completed">Completed</option>
                    <option value="incomplete">Incomplete</option>
                </select>
            </div>

            <ul style={{ listStyle: 'none', padding: 0 }}>
                {filteredTodos.map(todo => (
                    <li
                        key={todo.id}
                        style={{
                            display: 'flex',
                            alignItems: 'center',
                            justifyContent: 'space-between',
                            padding: '10px',
                            margin: '10px 0',
                            backgroundColor: '#f9f9f9',
                            border: '1px solid #ddd',
                            borderRadius: '4px',
                            textDecoration: todo.isCompleted ? 'line-through' : 'none',
                            opacity: todo.isCompleted ? 0.7 : 1,
                        }}
                    >
                        <div style={{ flex: 1 }}>
                            <input
                                type="checkbox"
                                checked={todo.isCompleted}
                                onChange={() => toggleCompletion(todo.id)}
                                style={{ marginRight: '10px', cursor: 'pointer' }}
                            />
                            {todo.content}
                        </div>
                        <div>
                            <button
                                onClick={() => editTodo(todo)}
                                style={{
                                    padding: '5px 10px',
                                    backgroundColor: '#ffc107',
                                    color: '#000',
                                    border: 'none',
                                    borderRadius: '4px',
                                    cursor: 'pointer',
                                    marginRight: '5px',
                                }}
                            >
                                Edit
                            </button>
                            <button
                                onClick={() => deleteTodo(todo.id)}
                                style={{
                                    padding: '5px 10px',
                                    backgroundColor: '#dc3545',
                                    color: '#fff',
                                    border: 'none',
                                    borderRadius: '4px',
                                    cursor: 'pointer',
                                }}
                            >
                                Delete
                            </button>
                        </div>
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default App;