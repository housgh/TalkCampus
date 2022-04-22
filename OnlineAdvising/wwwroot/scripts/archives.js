import {appSettings} from "./appSettings.js";

class Archives{
    
    startDate = null;
    
    constructor() {
        let userId = localStorage.getItem("userId");
        if(!userId) location.href = "/html/login.html";
        this.getArchiveChats();
    }
    
    getArchiveChats = () => {
        $.ajax({
            url: `${appSettings.apiUrl}Chat`,
            method: 'GET',
            success: this.onGetArchiveChatsSuccess,
            headers: {userId: localStorage.getItem("userId")}
        });
    }
    
    
    onGetArchiveChatsSuccess = (data) => {
        let oldestChatId = Number.MAX_SAFE_INTEGER;
        data.forEach(element => {
            oldestChatId = Math.min(oldestChatId, element.id);
            let chat = document.createElement("div");
            chat.className = "row chat-row";
            let heading = document.createElement("h6");
            heading.style = "font-weight: 600";
            heading.innerText = "Chat";
            let time = document.createElement("p");
            time.style = "font-size: 12px";
            let date = new Date(element.firstMessageSentAt);
            console.log(date)
            let timeText = date ? `${date.getDate()}/${date.getMonth()}/${date.getFullYear()} ${date.getHours()}:${date.getMinutes()}` : "";
            time.innerText = timeText;
            chat.append(heading);
            chat.append(time);
            chat.onclick = () => this.onChatClick(element);
            $("#chats").append(chat);
            $("#chats").append("<hr/>");
        });
        var date = new Date(data.find(x => x.id == oldestChatId)?.firstMessageSentAt);
        $("#dateStarted").text(!isNaN(date)? `${date.getDate()}/${date.getMonth()}/${date.getFullYear()} ${date.getHours()}:${date.getMinutes()}` : "");
        this.getArchiveMessages(oldestChatId);
    }
    
    getArchiveMessages = (id) => {
        if(id == Number.MAX_SAFE_INTEGER) return;
        $.ajax({
            url: `/api/Message/${id}`,
            method: 'GET',
            success: this.onGetArchiveMessagesSuccess,
            headers: {userId: localStorage.getItem("userId")}
        });
    }
    
    onGetArchiveMessagesSuccess = (data) => {
        $(".messages-container").html("");
        let userId = localStorage.getItem("userId");
        data.forEach(element => 
        {
            let isUserMessage = element.senderId == userId;
            let row = document.createElement("div");
            row.className = "row";
            if(isUserMessage){
                let space = document.createElement("div");
                space.className = "col-6";
                row.append(space);
            }
            let messageContainer = document.createElement("div");
            messageContainer.className = "col-6";
            let message = document.createElement("div");
            message.className = `chat-message-${isUserMessage? "right" : "left"} chat-message`;
            message.innerText = element.messageText;
            let date = new Date(element.sentAt);
            let dateText = `${date.getHours()}:${date.getMinutes()}`;
            let sub = document.createElement("sub");
            sub.innerText = dateText;
            message.append(sub);
            messageContainer.append(message);
            row.append(messageContainer);
            $(".messages-container").append(row);
        });
    }
    
    onChatClick = (chat) => {
        var date = new Date(chat.firstMessageSentAt);
        $("#dateStarted").text(!isNaN(date) ? `${date.getDate()}/${date.getMonth()}/${date.getFullYear()} ${date.getHours()}:${date.getMinutes()}` : "");
        this.getArchiveMessages(chat.id);
    }
    
    
}

let archivesModule;

$(document).ready(() => {
    archivesModule = new Archives();
})