var app = angular.module("ABET", ["ngRoute"]);
app.controller('mainCtrl', function ($scope, $http) {
    $scope.nextSemester = 3;
    //$scope.semesters = [{ id: 1, name: 'Fall2019' }, { id: 2, name: 'Spring2020' }, { id: 3, name: 'Summer2020' }];
    $http({
        method: 'GET',
        url: 'api/Semester/Get'
    }).then(function success(response) {
        $scope.semesters = response.data;
    }, function failure() {

    });

    $http({
        method: 'GET',
        url:'api/Semester/GetById/2'
    }).then(function success(response) {
        $scope.selectedSemester = response.data;
    }, function failure() {

    });

    

    $scope.selectSemester = function (semester) {
        $scope.addingSemester = false;
        $scope.selectedClass = undefined;
        $scope.selectedSemester = semester;
    }

    $scope.classes = [{ id: 0, course: { id: 0, courseName: 'Software Engineering 1', courseCode: 'CEN 4020' }, semesterId: 2, instructor: 'Chris Mills', syllabus: null, canvasLink: '', enrollment: 120 },
        { id: 1, course: { id: 1, courseName: 'C# Application Development', courseCode: 'CIS 4930' }, semesterId: 1, instructor: 'Chris Mills', syllabus: null, canvasLink: '', enrollment: 17 }];

    $scope.selectClass = function (cl) {
        $scope.addingClass = false;
        $scope.selectedClass = cl;
    }

    $scope.showNewClass = function () {
        $scope.addingClass = true;
        $scope.selectedClass = new Object();
        $scope.selectedClass.course = new Object();
    }

    $scope.addClass = function () {
        $scope.selectedClass.semesterId = $scope.selectedSemester.id;
        $scope.classes.push($scope.selectedClass);
        $scope.selectedClass = undefined;
        $scope.addingClass = false;
    }

    $scope.cancelAddClass = function () {
        $scope.selectedClass = undefined;
        $scope.addingClass = false;
    }

    $scope.showNewSemester = function () {
        $scope.selectedSemester = undefined;
        $scope.addingSemester = true;
    }
    $scope.addSemester = function () {
        //$scope.selectedSemester.id = $scope.nextSemester;
        //$scope.nextSemester += 1;
        

        $http({
            method: 'POST',
            url: 'api/Semester/AddOrUpdate',
            data: $scope.selectedSemester
        }).then(function success(response) {
            $scope.semesters.push(response.data);
        }, function failure() {

        });

        $scope.selectedSemester = undefined;
        $scope.addingSemester = false;
    }

    $scope.cancelAddSemester = function () {
        $scope.selectedSemester = undefined;
        $scope.addingSemester = false;
    }

    $scope.removeSemester = function (id) {
        var indexToDelete = -1;
        for (i = 0; i < $scope.semesters.length; i++) {
            if ($scope.semesters[i].id === id) {
                indexToDelete = i;
                break;
            }
        }
        if (indexToDelete >= 0) {
            $scope.semesters.splice(indexToDelete, 1);
        }

    }

    $scope.removeClass = function (id) {
        var indexToDelete = -1;
        for (i = 0; i < $scope.classes.length; i++) {
            if ($scope.classes[i].id === id) {
                indexToDelete = i;
                break;
            }
        }
        if (indexToDelete >= 0) {
            $scope.classes.splice(indexToDelete, 1);
        }

        if ($scope.selectedClass.id == id) {
            $scope.selectedClass = undefined;
        }
    }
});
