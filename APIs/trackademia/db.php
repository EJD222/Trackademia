<?php
$servername = "localhost";
$username = "testuser";
$password = "testuser";
$dbname = "trackademia";

$conn = new mysqli($servername, $username, $password, $dbname);

if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}
?>

