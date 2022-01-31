import { TodoModel } from "../../types/apiClient";

export interface HttpResponse<T> {
    readonly ok: boolean;
    readonly status: number;
    readonly body: T | null;
}

export const useApiClient = () => {
     
    const baseUrl = "https://localhost:7192/";
    const todoApriUrl = baseUrl + "Todo";


    async function getTodos(): Promise<HttpResponse<TodoModel[]>> {
        return sentHttpRequest<TodoModel[]>(
            "GET",
            todoApriUrl
        );
    }

    async function addTodos(text: string): Promise<HttpResponse<TodoModel>> {

        return sentHttpRequest(
            "POST",
            `${todoApriUrl}?text=${text}`
        );
    }

    async function removeTodo(guid: string) {
        console.log(guid);
        return sentHttpRequest(
            "DELETE",
            `${todoApriUrl}/${guid}`
        );
    }

    async function sentHttpRequest<T>(
        verb: string,
        url: string,
        payload?: any
    ): Promise<HttpResponse<T>> {
        var request = new Request(url, {
            method: verb,
            body: payload ? undefined : JSON.stringify(payload),
            headers: {
                Accept: "application/json",
                "Content-Type": "application/json"
            },
        });

        const fetchResponse = await fetch(request);

        const response = {
            body: await fetchResponse.json(),
            ok: fetchResponse.ok,
            status: fetchResponse.status,
        } as HttpResponse<T>;

        try {
            if (!response.ok) {
                if (response.status !== 422 && response.status !== 404) {
                    console.log("Error on api");
                }
            }
        } catch (error) {
            throw error;
        }

        return response;
    }

    return {
        getTodos,
        addTodos,
        removeTodo
    };
};
