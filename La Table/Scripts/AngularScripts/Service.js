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

    // DELETE ACCOUNT
    this.deleteUser = function (accID) {
        return $http({
            method: 'POST',
            url: '/Home/DeleteUser',
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

    // ADD PROMO
    this.postPromoData = function (formData) {
        return $http({
            method: 'POST',
            url: '/Home/AddPromo',
            data: formData,
            headers: { 'Content-Type': undefined },
            transformRequest: angular.identity 
        });
    };

    // EDIT PROMO
    this.updatePromo = function (editPromoID, editPromoName, editpromoDescription, editstartDate, editendDate ) {
        var response = $http({
            method: "post",
            url: "/Home/updatePromo",
            params: {
                editID: editPromoID,
                editPromoName: editPromoName,
                editpromoDescription: editpromoDescription,
                editstartDate: editstartDate,
                editendDate: editendDate,
            }
        });
        return response;
    };

    // DELETE PROMO
    this.deletePromo = function (promoID) {
        return $http({
            method: 'POST',
            url: '/Home/DeletePromo',
            data: { PromoID: promoID }
        });
    };

    // LIST BOOKINGS
    this.getBookings = function () {
        return $http({
            method: 'GET',
            url: '/Home/GetBookings'
        });
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

    // DELETE TABLE
    this.deleteTable = function (tableID) {
        return $http({
            method: 'POST',
            url: '/Home/DeleteTable',
            data: { TableID: tableID }
        });
    };

    // MAIN BOOKING TRIAL
    this.reserveTable = function (reservation) {
        return $http.post('/Home/ReserveTable', reservation);

    };

    $scope.getReservationDetails = function (reservationId) {
        return $http.get("/Home/GetReservationDetails", {
            params: { reservationId: reservationId }
        });
    };

    $scope.updateReservation = function (data) {
        return $http.post("/Home/UpdateReservation", data);
    };


});