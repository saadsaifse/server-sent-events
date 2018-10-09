var eventSource = new EventSource("/updates");

eventSource.onmessage = function (event) {
    appendToOutput(event.data);

    if (event.id == "CLOSE") {
        source.close();
    }
};

eventSource.onopen = function () {appendToOutput('*** Connected ***'); };
eventSource.onerror = function () {appendToOutput('*** Error ***'); };

var appendToOutput = function (msg) {
    var div = document.getElementById("updates");
    var pre = document.createElement("pre");
    pre.appendChild(document.createTextNode(msg));

    div.appendChild(pre);
    div.appendChild(document.createElement('hr'));
};

function sendPing() {
    var xhttp = new XMLHttpRequest();
    xhttp.open("POST", "/Events/Ping", true);
    xhttp.send();
}

function sendMessage() {
    var textArea = document.getElementById("message");

    if (!textArea.value || 0 === textArea.value.length) {
        alert("Message is empty");
        return;
    }

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            textArea.value = "";
        }
    };
    xhttp.open("POST", "/Events/Message", true);
    xhttp.setRequestHeader("Content-Type", "application/json; charset=utf-8");
    xhttp.send(JSON.stringify(textArea.value));
}
