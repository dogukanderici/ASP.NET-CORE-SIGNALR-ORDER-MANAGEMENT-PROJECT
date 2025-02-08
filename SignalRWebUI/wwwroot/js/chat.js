var connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:7098/SignalRHub").build();
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var currentTime = new Date();

    var li = document.createElement("li");
    var span = document.createElement("span");

    span.style.fontWeight = "bold";
    span.textContent = user;

    li.appendChild(span);
    li.innerHTML += `${message} - ${currentTime.getHours()}:${currentTime.getMinutes()}`;

    document.getElementById("messageList").appendChild(li);
});

connection.start()
    .then(() => {
        document.getElementById("sendButton").disabled = false;
    })
    .catch((err) => { console.error(err) });

document.getElementById("sendButton").addEventListener("click", function (event) {
    let user = document.getElementById("inputUserName").value;
    let message = document.getElementById("inputMessage").value;

    connection.invoke("SendMessage", user, message)
        .catch((err) => { console.error(err) });

    event.preventDefault();
});