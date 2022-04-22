import {appSettings} from "./appSettings.js";

class Chat {

    chatId = null;
    chatData = null;
    lastMessageId = -1;
    isAppending = false;

    constructor() {
        let userId = localStorage.getItem("userId");
        let roleId = localStorage.getItem("roleId");
        if (!userId || !roleId) location.href = "/html/login.html";
        if(roleId == 1) $("#rate").css("display","none");
        const params = new Proxy(new URLSearchParams(window.location.search), {
            get: (searchParams, prop) => searchParams.get(prop),
        });
        this.chatId = params.chatId
        if(!this.chatId) location.href = "/html/index.html";
        this.getChatData(this.chatId);
        this.bindEvents();
        setInterval(this.getNewMessages, 2000);
    }
    
    bindEvents = () => {
        $("#send").click(this.sendMessage);
        $("#end").click(this.endChat);
        $("#report").click(this.reportSecondParty);
        $("#rate").click(this.ratePsychologist);
    }

    getChatData = (id) => {
        $.ajax({
            url: `${appSettings.apiUrl}Chat/${id}`,
            method: 'GET',
            success: this.onGetChatDataSuccess,
            headers: {userId: localStorage.getItem("userId")}
        });
    }

    getNewMessages = () => {
        if(this.isAppending) return;
        $.ajax({
            url: `${appSettings.apiUrl}Message/${this.chatId}/${this.lastMessageId}`,
            method: 'GET',
            success: this.onGetNewMessagesSuccess,
            headers: {userId: localStorage.getItem("userId")}
        });
    }
    sendMessage = () => {
        let model = {
            messageText: $("#messageText").val(),
            senderId: localStorage.getItem("userId"),
            chatId: this.chatId
        }
        $.ajax({
            url: `${appSettings.apiUrl}Message`,
            method: 'POST',
            data: JSON.stringify(model),
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            headers: {userId: localStorage.getItem("userId")}
        });
        $("#messageText").val("")
    }

    reportSecondParty = () => {
        let reason = prompt("Enter your reason:");
        if(reason === null) return;
        if(!reason) {
            alert("reason is required");
            return;
        }
        let model = {
            chatId: this.chatId,
            reason
        };
        $.ajax({
            url: `${appSettings.apiUrl}Chat/Report`,
            method: 'POST',
            data: JSON.stringify(model),
            dataType: 'text',
            contentType: "application/json; charset=utf-8",
            success: this.onReportSuccess,
            headers: {userId: localStorage.getItem("userId")}
        });
    }

    ratePsychologist = () => {
        let rateValueStr = prompt("Enter a value (1-5):");
        if(rateValueStr === null) return;
        if(!rateValueStr) {
            alert("Value is required");
            return;
        }
        let rateValue = parseFloat(rateValueStr);
        if(isNaN(rateValue)) {
            alert("Invalid Value");
            return;
        }
        if(rateValue < 1 || rateValue > 5) {
            alert("Value must be between 1 and 5");
            return;
        }
        let model = {
            chatId: this.chatId,
            rateValue
        };
        $.ajax({
            url: `${appSettings.apiUrl}Chat/Rate`,
            method: 'POST',
            data: JSON.stringify(model),
            dataType: 'text',
            contentType: "application/json; charset=utf-8",
            success: this.onRateSuccess,
            headers: {userId: localStorage.getItem("userId")}
        });
    }

    onGetChatDataSuccess = (data) => {
        this.chatData = data;
        this.chatId = data.id;
        $("#dateStarted").html(this.chatData.firstMessageSent)
    }

    onGetNewMessagesSuccess = (data) => {
        this.isAppending = true;
        var userId = localStorage.getItem("userId");
        data.forEach(element => {
            let isUserMessage = element.senderId == userId;
            let row = document.createElement("div");
            row.className = "row";
            if (isUserMessage) {
                let space = document.createElement("div");
                space.className = "col-6";
                row.append(space);
            }
            let messageContainer = document.createElement("div");
            messageContainer.className = "col-6";
            let message = document.createElement("div");
            message.className = `chat-message-${isUserMessage ? "right" : "left"} chat-message`;
            message.innerText = element.messageText;
            let date = new Date(element.sentAt);
            let dateText = `${date.getHours()}:${date.getMinutes() < 9 ? "0":""}${date.getMinutes()}`;
            let sub = document.createElement("sub");
            sub.innerText = dateText;
            message.append(sub);
            messageContainer.append(message);
            row.append(messageContainer);
            $(".messages-container").append(row);
            this.lastMessageId = element.id;
        });
        this.isAppending = false;
        var element = document.getElementById('messagesContainer');
        element.scrollTop = element.scrollHeight;
    }
    
    endChat = () => {
        let endChat = confirm("End Chat?");
        if(!endChat) return;
        $.ajax({
            url: `${appSettings.apiUrl}Appointment/End/${this.chatId}`,
            method: "PUT",
            success: this.onEndChatSuccess,
            headers: {userId: localStorage.getItem("userId")}
        });
    }
    
    onEndChatSuccess = () => {
        location.href = "/html/index.html";
    }

    onReportSuccess = (data) => {
        alert("Report Successful");
        location.href = "/html/index.html";
    }

    onRateSuccess = (data) => {
        alert("Rating Submited");
        $("#rate").css("display","none");
    }

}

let chatModule;

$(document).ready(() => {
    chatModule = new Chat();
})