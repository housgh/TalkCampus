import {appSettings} from "./appSettings.js";

class Users {
    constructor() {
        var userId = localStorage.getItem("userId");
        var roleId = localStorage.getItem("roleId");
        if(!userId || !roleId || roleId != 3) location.href = "/html/login.html";
        this.bindEvents();
        this.getUser();
    }

    bindEvents = () => {
        $("#tabs").tabs();
    }

    getUser = () => {
        $.ajax({
            url: `${appSettings.apiUrl}Dashboard/Admin`,
            method: 'GET',
            success: this.onGetUsers,
            headers: {userId: localStorage.getItem("userId")}
        })
    }
    
    onGetUsers = (data) => {
        data.psychologists.forEach(e => {
            this.addPsychologistRow(e);
        });
        
        data.patients.forEach(e => {
            this.addPatienttRow(e);
        })
    }
    
    addPatienttRow = (data) => {
        let table = document.getElementById("patientsTable");
        let row = table.insertRow();
        this.addCellToRow(row, data.id);
        this.addCellToRow(row, data.fullName);
        this.addCellToRow(row, data.email);
        this.addCellToRow(row, data.appointmentsCount);
        this.addCellToRow(row, data.timesReported);
    }

    addPsychologistRow = (data) => {
        console.log(data)
        let table = document.getElementById("psychologistsTable");
        let row = table.insertRow();
        this.addCellToRow(row, data.id);
        this.addCellToRow(row, data.fullName);
        this.addCellToRow(row, data.email);
        this.addCellToRow(row, data.degree);
        this.addCellToRow(row, data.hoursServed);
        this.addCellToRow(row, data.studentsHelped);
        this.addCellToRow(row, data.rating);
        this.addCellToRow(row, data.timesReported);
        this.addCellToRow(row, data.status);
        let actionsCell = row.insertCell();
        let filesButton = document.createElement("button");
        filesButton.innerText = "View Files";
        filesButton.className = "btn btn-primary";
        filesButton.onclick = () => this.onFilesButtonClick(data.files);
        actionsCell.append(filesButton);
        if(data.status == 3){
            let activateButton = document.createElement("button");
            activateButton.innerText = "Activate";
            activateButton.className = "btn btn-success";
            activateButton.style = "margin-left: 5%";
            activateButton.onclick = () => this.onActivateButtonClick(data.id);
            actionsCell.append(activateButton);
        }
    }
    
    addCellToRow = (row, cellData) => {
        let cell = row.insertCell();
        cell.innerText = cellData;
    }

    onActivateButtonClick = (id) => {
        $.ajax({
            url: `${appSettings.apiUrl}Dashboard/Activate/${id}`,
            method: 'PUT',
            success: () => location.reload(),
            headers: {userId: localStorage.getItem("userId")}
        })
    }

    onFilesButtonClick = (files) => {
        let list = document.createElement("ul");
        files.forEach(x => {
            let listItem = document.createElement("li");
            let downloadLink = document.createElement("button");
            downloadLink.innerText = x.name;
            downloadLink.className = "btn btn-link";
            downloadLink.onclick = () => {
                let sampleArr = this.base64ToArrayBuffer(x.value);
                this.saveByteArray("file", sampleArr);
            }
            listItem.append(downloadLink);
            list.append(listItem);
        })
        $(".modal-body").append(list);
        $("#modal").modal("show");
    }

    base64ToArrayBuffer = (base64) =>  {
        let binaryString = window.atob(base64);
        let binaryLen = binaryString.length;
        let bytes = new Uint8Array(binaryLen);
        for (let i = 0; i < binaryLen; i++) {
            let ascii = binaryString.charCodeAt(i);
            bytes[i] = ascii;
        }
        return bytes;
    }

    saveByteArray = (reportName, byte) => {
        var blob = new Blob([byte], {type: "application/pdf"});
        var link = document.createElement('a');
        link.href = window.URL.createObjectURL(blob);
        var fileName = reportName;
        link.download = fileName;
        link.click();
    };
}

let usersModule = null;

$(document).ready(() => {
    usersModule = new Users();
})