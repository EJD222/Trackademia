<?php
header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json; charset=UTF-8");

include 'db.php';

// Retrieve the student ID from the request
$id = isset($_GET['id']) ? intval($_GET['id']) : 0;

if ($id > 0) {
    $sql = "SELECT tblattendance.ID AS AttendanceID, tblattendance.Date, tblattendance.Status, 
                   tbluser.ID AS StudentID, tbluser.Name AS StudentName, tbluser.StudentId AS StudentNumber 
            FROM tblattendance
            LEFT JOIN tbluser ON tblattendance.StudentID = tbluser.ID
            WHERE tblattendance.StudentID = $id";

    $result = $conn->query($sql);

    $attendances = [];
    while ($row = $result->fetch_assoc()) {
        $attendances[] = $row;
    }

    if (!empty($attendances)) {
        echo json_encode($attendances);
    } else {
        echo json_encode(["message" => "No attendance records found for the given student ID"]);
    }
} else {
    echo json_encode(["message" => "Invalid student ID"]);
}

$conn->close();
?>
