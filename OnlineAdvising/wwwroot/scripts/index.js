class Index{
    constructor() {
        let userId = localStorage.getItem("userId");
        let roleId = localStorage.getItem("roleId");
        
        if(!userId || !roleId){
            localStorage.clear();
            location.href = "/html/login.html";
        }
        let html = roleId == 1? "/html/psychologist-dashboard.html" : "/html/patient-dashboard.html";
        $("body").load(html);
    }
}

new Index();