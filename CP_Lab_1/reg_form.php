<?php
session_start();
function redirect($location)
{
    header('Location: '. $location);
    exit;
}
unset($_SESSION['empty_fields_reg']);
unset($_SESSION['user_exists']);
require_once('db.php');

$login = $_POST['login'];
$password = $_POST['password'];

$sql = "SELECT * FROM `users` WHERE login = '$login'";
$result = $conn_users->query($sql);
if ($result->num_rows > 0)
{
    $_SESSION['user_exists'] = 1;
    redirect('registration.php');
}

if (empty($login) || empty($password))
{
    $_SESSION['empty_fields_reg'] = 1;
    redirect('registration.php');
}

$sql = "INSERT INTO `users` (login, password) VALUES ('$login', '$password')";
$conn_users -> query($sql);
$sql = "SELECT * FROM `users` WHERE login = '$login'";
$result = $conn_users -> query($sql);
while ($row = mysqli_fetch_array($result))
{
    $_SESSION['user_id'] = $row['id'];
}

redirect('index.php');

?>