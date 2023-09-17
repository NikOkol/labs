<?php

$servername = "localhost";
$username = "root";
$password = "root";

$conn_users = mysqli_connect($servername, $username, $password, "users");
$conn_messages = mysqli_connect($servername, $username, $password, "messages");
?>