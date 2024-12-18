<?php
include 'db.php';

$data = json_decode(file_get_contents("php://input"));

$date = $data->date;
$studentId = $data->studentId;
$status = $data->status;

$sql = "INSERT INTO tblattendance (Date, StudentID, Status) VALUES ('$date', $studentId, '$status')";

if ($conn->query($sql) === TRUE) {
    echo json_encode(["message" => "Attendance record added successfully"]);
} else {
    echo json_encode(["message" => "Error: " . $conn->error]);
}

$conn->close();
?>
