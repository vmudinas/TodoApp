import React from 'react';
import './App.css';
import { HttpResponse, useApiClient } from './Services/api/apiClient';
import { useEffect, useState } from 'react';
import { TodoModel } from './types/apiClient';
import { KeyboardEvent } from "react";
import { Button } from '@material-ui/core';

 function App() {

     let todosApi = useApiClient();
     const [todos, setTodos] = useState<TodoModel[]>([]);

     const getTodos = async () => {
         let response: HttpResponse<TodoModel[]>;

         response = await todosApi.getTodos();
         setTodos(response.body as TodoModel[]);      
     };

     const addTodos = async (text: string): Promise<HttpResponse<TodoModel>> => {
         return (await todosApi.addTodos(text));
     };

     const removeTodo = async (guid: string) => {
         await todosApi.removeTodo(guid);
     };
 
     useEffect(() => {
         getTodos();
     }, [])

     const [inputValue, setInputValue] = useState("");      

    const handleInputChange = (e: { currentTarget: { value: React.SetStateAction<string>; }; }) => {
        setInputValue(e.currentTarget.value);
    }

    const handleComplete = (e: { currentTarget: { getAttribute: (arg0: string) => any; }; }) => {
        const index = e.currentTarget.getAttribute("data-index");
        const newTodos = [...todos];
        newTodos[index].completed = !newTodos[index].completed;
        setTodos(newTodos);
    };

    const handleRemove = (e: { stopPropagation: () => void; currentTarget: { getAttribute: (arg0: string) => any; }; }) => {
        e.stopPropagation();
        const index = e.currentTarget.getAttribute("data-index");
        var taskToDelete = todos[index];
        const newTodos = [...todos];
        newTodos.splice(index, 1);
        setTodos(newTodos);
        removeTodo(taskToDelete.id);
    };

     const handleSubmit = async (e: { preventDefault: () => void; }) => {
            e.preventDefault();
            if (!inputValue) return;
         var addedTodo = await addTodos(inputValue);

         const newTodos = [...todos];
         if (addedTodo?.body) {
             newTodos.push(addedTodo?.body);
             setTodos(newTodos);
             setInputValue('');
         }
    };

    return (
        <div className="contain">
            <form onSubmit={handleSubmit}>
                <input
                    type="text"
                    value={inputValue}
                    onChange={handleInputChange}
                />
            </form>
            <ul>                
                {todos.map(({ text, completed, id }, i) => (
                    <li
                        aria-role="button"
                        className={completed ? 'complete' : ""}
                        data-index={i}
                        onClick={handleComplete}
                    >
                        <div>{text}</div>

                        <button
                            aria-label={`remove todo ${i}`}
                            className="remove"
                            data-index={i}
                            onClick={handleRemove}
                        />
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default App;
