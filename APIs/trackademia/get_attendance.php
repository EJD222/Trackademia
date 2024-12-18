<?php
header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json; charset=UTF-8");

include 'db.php';

$sql = "SELECT tblattendance.ID, tblattendance.Date, tblattendance.Status, 
               tbluser.Name AS StudentName, tbluser.StudentId AS StudentNumber 
        FROM tblattendance
        LEFT JOIN tbluser ON tblattendance.StudentID = tbluser.ID";
$result = $conn->query($sql);

$attendances = [];
while ($row = $result->fetch_assoc()) {
    $attendances[] = $row;
}

echo json_encode($attendances);

$conn->close();
?>
