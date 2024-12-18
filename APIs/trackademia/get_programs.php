<?php
header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json; charset=UTF-8");

include 'db.php';

$sql = "SELECT ID, ProgramName, ProgramCode FROM tblprogram";
$result = $conn->query($sql);

$programs = [];
while ($row = $result->fetch_assoc()) {
    $programs[] = $row;
}

echo json_encode($programs);

$conn->close();
?>
