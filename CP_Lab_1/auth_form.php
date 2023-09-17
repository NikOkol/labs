<?php
session_start();
function redirect($location)
{
    header('Location: '. $location);
    exit;
}
unset($_SESSION['empty_fields_login']);
unset($_SESSION['not_exists']);
unset($_SESSION['invalid_pass']);
require_once('db.php');

$login = $_POST['login'];
$password = $_POST['password'];


$sql = "SELECT * FROM `users` WHERE login = '$login'";
$result = $conn_users->query($sql);
if ($result->num_rows == 0)
{
    $_SESSION['not_exists'] = 1;
    redirect('authorization.php');
}

if (empty($login) || empty($password))
{
    $_SESSION['empty_fields_login'] = 1;
    redirect('authorization.php');
}

while ($row = mysqli_fetch_array($result))
{
    if ($password != $row['password'])
    {
        $_SESSION['invalid_pass'] = 1;
        redirect('authorization.php');
    }
    else
    {
        $_SESSION['user_id'] = $row['id'];
        redirect('index.php');
    }
}
?>