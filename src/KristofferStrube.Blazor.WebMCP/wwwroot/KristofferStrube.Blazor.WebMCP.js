export function registerTool(tool, options) {
    navigator.modelContext.registerTool({
        "name": tool.name,
        "description": tool.description,
        "inputSchema": tool.inputSchema,
        "execute": async (input, client) => {
            if (client != undefined && client != null) {
                client = DotNet.createJSObjectReference(client);
            }
            return await tool.objRef.invokeMethodAsync("InvokeExecuteAsync", input, client)
        },
        "annotations": tool.annotations == null ? undefined : tool.annotations,
    }, options);
}

export async function requestUserInteraction(modelContextClient, callback) {
    return await modelContextClient.requestUserInteraction(async () => await callback.invokeMethodAsync("InvokeCallback"))
}

export function hasModelContext() {
    return navigator.modelContext == undefined;
}

export function hasRegisterToolFunction() {
    return navigator.modelContext.registerTool != undefined;
}

export function hasUnregisterToolFunction() {
    return navigator.modelContext.unregisterTool != undefined;
}