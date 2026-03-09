mergeInto(LibraryManager.library, {

    MyWS_Create: function(urlPtr) {
        var url = UTF8ToString(urlPtr);

        // tạo id socket
        var id = Math.random().toString(36).substr(2, 9);

        var ws = new WebSocket(url);
        ws.binaryType = "arraybuffer";

        // lưu vào bảng global
        Module['myws_' + id] = ws;

        ws.onopen = function() {
            SendMessage("WebGLSocketManager", "OnOpen", id);
        };

        ws.onmessage = function(e) {
            var msg = typeof e.data === "string" ? e.data : "";
            SendMessage("WebGLSocketManager", "OnMessage", id + "|" + msg);
        };

        ws.onclose = function() {
            SendMessage("WebGLSocketManager", "OnClose", id);
        };

        return allocateUTF8(id);
    },

    MyWS_Send: function(idPtr, msgPtr) {
        var id = UTF8ToString(idPtr);
        var msg = UTF8ToString(msgPtr);
        var ws = Module['myws_' + id];
        ws && ws.send(msg);
    },

    MyWS_Close: function(idPtr) {
        var id = UTF8ToString(idPtr);
        var ws = Module['myws_' + id];
        ws && ws.close();
        delete Module['myws_' + id];
    }
});