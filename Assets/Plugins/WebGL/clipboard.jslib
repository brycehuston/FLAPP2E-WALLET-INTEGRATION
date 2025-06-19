mergeInto(LibraryManager.library, {
    GetClipboard: function () {
        if (navigator.clipboard && navigator.clipboard.readText) {
            navigator.clipboard.readText().then(function (text) {
                alert("Pasted: " + text); // No emoji!
                window.unityInstance.SendMessage("ClipboardHandler", "SetClipboardText", text);
            }).catch(function (err) {
                alert("Clipboard Error: " + err);
            });
        } else {
            alert("Clipboard not supported.");
        }
    }
});
