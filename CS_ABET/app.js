var app = angular.module("ABET", ["ngRoute"]);
app.controller('mainCtrl', function ($scope, $http) {
    $scope.nextSubject = 3;
    
    $http({
        method: 'GET',
        url: 'api/Subject/Get'
    }).then(function success(response) {
        $scope.Subjects = response.data;
    }, function failure() {

    });

    $http({
        method: 'GET',
        url:'api/Course/Get'
    }).then(function success(response) {
        $scope.courses = response.data;
    }, function failure() {

    });

    

    $scope.selectSubject = function (Subject) {
        $scope.addingSubject = false;
        $scope.selectedClass = undefined;
        $scope.selectedSubject = Subject;

        $http({
            method: 'GET',
            url: 'api/Class/Get/' + $scope.selectedSubject.Id
        }).then(
            function success(response) {
                $scope.classes = response.data;
                for (j = 0; j < $scope.classes.length; j++) {
                    for (i = 0; i < $scope.courses.length; i++) {
                        if ($scope.courses[i].Id == $scope.classes[j].CourseId) {
                            $scope.classes[j].Course = $scope.courses[i]
                            break;
                        }
                    }
                }
            }, function failure() {

            }
        );
    }

    $scope.classes = [{ id: 0, course: { id: 0, courseName: 'Software Engineering 1', courseCode: 'CEN 4020' }, SubjectId: 2, instructor: 'Chris Mills', syllabus: null, canvasLink: '', enrollment: 120 },
        { id: 1, course: { id: 1, courseName: 'C# Application Development', courseCode: 'CIS 4930' }, SubjectId: 1, instructor: 'Chris Mills', syllabus: null, canvasLink: '', enrollment: 17 }];

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

        $http({
            method: "POST",
            url: 'api/Class/AddOrUpdate',
            data: {
                Id: $scope.selectedClass.id,
                SubjectId: $scope.selectedSubject.Id,
                CourseId: $scope.selectedClass.Course.Id,
                Instructor: $scope.selectedClass.Instructor,
                Enrollment: $scope.selectedClass.Enrollment
            }
        }).then(
            function success(response) {
                var result = response.data;
                for (i = 0; i < $scope.courses.length; i++) {
                    if ($scope.courses[i].Id == result.CourseId) {
                        result.Course = $scope.courses[i]
                        break;
                    }
                }
                $scope.classes.push(result);
            }, function failure() {

            }
        );

        $scope.selectedClass = undefined;
        $scope.addingClass = false;
    }

    $scope.cancelAddClass = function () {
        $scope.selectedClass = undefined;
        $scope.addingClass = false;
    }

    $scope.showNewSubject = function () {
        $scope.selectedSubject = undefined;
        $scope.addingSubject = true;
    }
    $scope.addSubject = function () {
        //$scope.selectedSubject.id = $scope.nextSubject;
        //$scope.nextSubject += 1;
        

        $http({
            method: 'POST',
            url: 'api/Subject/AddOrUpdate',
            data: $scope.selectedSubject
        }).then(function success(response) {
            $scope.Subjects.push(response.data);
        }, function failure() {

        });

        $scope.selectedSubject = undefined;
        $scope.addingSubject = false;
    }

    $scope.cancelAddSubject = function () {
        $scope.selectedSubject = undefined;
        $scope.addingSubject = false;
    }

    $scope.removeSubject = function (id) {

        $http({
            method: 'GET',
            url: 'api/Subject/RemoveById/' + id
        }).then(function success() {
            var indexToDelete = -1;
            for (i = 0; i < $scope.Subjects.length; i++) {
                if ($scope.Subjects[i].Id === id) {
                    indexToDelete = i;
                    break;
                }
            }
            if (indexToDelete >= 0) {
                $scope.Subjects.splice(indexToDelete, 1);
            }

            if (id === $scope.selectedSubject.Id) {
                $scope.selectedSubject = undefined;
            }
        }, function failure() {

        });
        

    }

    $scope.removeClass = function (id) {

        $http({
            method: 'GET',
            url: 'api/Class/Remove/' + id
        }).then(
            function success(response) {
                var indexToDelete = -1;
                for (i = 0; i < $scope.classes.length; i++) {
                    if ($scope.classes[i].Id === id) {
                        indexToDelete = i;
                        break;
                    }
                }
                if (indexToDelete >= 0) {
                    $scope.classes.splice(indexToDelete, 1);
                }

                if ($scope.selectedClass !== undefined && $scope.selectedClass.Id == id) {
                    $scope.selectedClass = undefined;
                }
            }, function failure() {

            }
        );

        
    }
});
