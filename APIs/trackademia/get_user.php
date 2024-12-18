
<?php
header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json; charset=UTF-8");

include 'db.php';

$sql = "SELECT tbluser.ID, tbluser.Name, tbluser.Email, tbluser.StudentId, 
               tbluser.Address, tbluser.Birthdate, tbluser.Program, tblprogram.ProgramName, tblprogram.ProgramCode 
        FROM tbluser 
        LEFT JOIN tblprogram ON tbluser.Program = tblprogram.ID";
$result = $conn->query($sql);

$users = [];
while ($row = $result->fetch_assoc()) {
    $users[] = $row;
}

echo json_encode($users);

$conn->close();

?>