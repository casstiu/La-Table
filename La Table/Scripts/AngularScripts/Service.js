app.service("LaTableService", function ($http) {

    // CHECK IF EMAIL EXISTS
    this.checkEmailExists = function (email) {
        return $http.get("/Home/CheckEmailExists", { params: { email: email } });
    };

    // REGISTER
    this.registerUser = function (user) {
        return $http.post("/Home/RegisterUser", user);
    };

    // LOGIN
    this.loginUser = function (credentials) {
        return $http.post("/Home/Login", credentials);
    };

    // LIST ACCOUNTS
    this.getUsers = function () {
        return $http({
            method: 'GET',
            url: '/Home/GetUsers'
        });
    };

    // ADD ACCOUNT
    this.addAccount = function (user) {
        return $http.post("/Home/AddAccount", user);
    };

    // EDIT ACCOUNT
    this.updateAcc = function (editAccID, editFirstName, editLastName, editPhone, editEmail) {
        var response = $http({
            method: "post",
            url: "/Home/updateAcc",
            params: {
                editID: editAccID,
                editFName: editFirstName,
                editLName: editLastName,
                editPhone: editPhone,
                editEmail: editEmail,
            }
        });
        return response;
    };

    // DEACTIVATE ACCOUNT
    this.deactivateUser = function (accID) {
        return $http({
            method: 'POST',
            url: '/Home/DeactivateUser',
            data: { AccountID: accID } 
        });
    };
    
    // LIST LOGS
    this.getLogs = function () {
        return $http({
            method: 'GET',
            url: '/Home/GetLogs'
        });
    };

    // LIST PROMOS
    this.getPromos = function () {
        return $http({
            method: 'GET',
            url: '/Home/GetPromos'
        });
    };

    // Post new promo
    this.postPromoData = function (formData) {
        return $http({
            method: 'POST',
            url: '/Home/AddPromo',
            data: formData,
            headers: { 'Content-Type': undefined },  // Angular will set the correct Content-Type
            transformRequest: angular.identity  // This is important to send FormData properly
        });
    };

    // Delete promo
    this.deletePromo = function (promoID) {
        return $http.delete('/Home/DeletePromo/' + promoID);
    };

    // LIST TABLES
    this.getTables = function () {
        return $http({
            method: 'GET',
            url: '/Home/GetTables'
        });
    };

    // ADD TABLE
    this.addTable = function (table) {
        return $http.post("/Home/AddTable", table);
    };

    // EDIT TABLE
    this.updateTable = function (editTableID, editSeatcap) {
        var response = $http({
            method: "post",
            url: "/Home/updateTable",
            params: {
                editID: editTableID,
                editSeatcap: editSeatcap,
            }
        });
        return response;
    };
    this.createReservation = function (reservationData) {
        return $http.post('/Home/reservations', reservationData);
    };
});
    