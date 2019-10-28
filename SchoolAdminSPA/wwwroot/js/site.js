// Write your Javascript code.

var schoolAdinCtrls = angular.module('schoolAdinCtrls', [])
    .controller('indexCtrl', indexCtrl)
    .controller('editCtrl', editCtrl)
    .controller('testCtrl', testCtrl)


function indexCtrl($scope, $http) {
    $scope.message = 'hello';

    $scope.deleteStudent = function (studentId) {
        $http.post('/student/DeleteStudent',  studentId )
    }

    function init() {
        $http.get("/student/GetStudents").then(function (data) {
            $scope.students = data.data;
        })
    }
    init();
}

function editCtrl($scope, $http, $location) {
    $scope.id = getUrlParameters().id;
    if ($scope.id == 'new') {
        $scope.new = true;
        $scope.action = "Add";
        $scope.student = {
            firstName: null,
            lastName: null,
            email: null,
            phoneNumber: null
        }
    } else {
        $scope.action = "Edit";
        $scope.new = false;
        $http.get("/student/GetStudent?id=" + parseInt($scope.id)).then(function (data) {
            $scope.student = data.data;
        })
    }

    $scope.saveStudent = function () {
        $http.post('/student/AddStudent', $scope.student).then(function () {
            window.location.href = '/home/index'
        })
    }

    $scope.editStudent = function () {
        $http.post('/student/EditStudent', $scope.student).then(function () {
            window.location.href = '/home/index'
        })
    }

    $scope.validate = function () {
        return $scope.student.firstName != null && $scope.student.firstName != ''
            && $scope.student.lastName != null && $scope.student.lastName != ''
            && $scope.student.email != null && $scope.student.email != '' && validateEmail($scope.student.email)
            && $scope.student.phoneNumber != null && $scope.student.phoneNumber != '' && validatePhoneNumber($scope.student.phoneNumber)
    }

    function getUrlParameters() {
        var pairs = window.location.search.substring(1).split(/[&?]/);
        var res = {}, i, pair;
        for (i = 0; i < pairs.length; i++) {
            pair = pairs[i].split('=');
            if (pair[1])
                res[decodeURIComponent(pair[0])] = decodeURIComponent(pair[1]);
        }
        return res;
    }

    function validateEmail(email) {
        var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(String(email).toLowerCase());
    }

    function validatePhoneNumber(number){
        var re = /^\d{10}$/
        return re.test(number);
    }
}

function testCtrl($scope, $http) {
    $scope.id = getUrlParameters().id;

    $scope.saveTest = function () {
        var test = {
            StudentId: $scope.student.studentId,
            Mark: $scope.newTest.mark,
            Subject: $scope.newTest.subject,
            Date: $scope.newTest.date
        }
        $http.post('/test/AddTest', test).then(function (data) {
            location.reload();
        })
    }

    function init() {
        $http.get("/test/GetStudent?id=" + parseInt($scope.id)).then(function (data) {
            $scope.student = data.data;
        })
    }
    init();

    function getUrlParameters() {
        var pairs = window.location.search.substring(1).split(/[&?]/);
        var res = {}, i, pair;
        for (i = 0; i < pairs.length; i++) {
            pair = pairs[i].split('=');
            if (pair[1])
                res[decodeURIComponent(pair[0])] = decodeURIComponent(pair[1]);
        }
        return res;
    }
}
