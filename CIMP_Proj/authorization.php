<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="main.css" rel="stylesheet">
    <title>Authorization</title>
</head>
<body>
    <a href="index.php">На главную</a>
    <h1>Вход в аккаунт</h1>
    <div class="form">
        <form action="auth_form.php" method="post">
            <input type="text" placeholder="Логин" name="login"><br>
            <input type="text" placeholder="Пароль" name="password">
            <?php
                session_start();
                if (isset($_SESSION['user_id']))
                {
                    header('Location: index.php');
                }
                if(isset($_SESSION['empty_fields_login']))
                {
                    echo '<p>Введены не все значения</p>';
                }
                if(isset($_SESSION['not_exists']))
                {
                    echo '<p>Пользователь не найден</p>';
                }
                if(isset($_SESSION['invalid_pass']))
                {
                    echo '<p>Неверный пароль</p>';
                }
            ?>
            <button type="submit">Войти</button>
        </form>
    </div>
    
</body>
</html>
