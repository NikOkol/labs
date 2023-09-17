<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="main.css" rel="stylesheet">
    <title>Registration</title>
</head>
<body>
    <a href="index.php">На главную</a>
    <h1>Регистрация</h1>
    <div class="form">
        <form action="reg_form.php" method="post">
            <input type="text" placeholder="Логин" name="login">
            <input type="text" placeholder="Пароль" name="password">
            <?php
                session_start();
                if (isset($_SESSION['user_id']))
                {
                    header('Location: index.php');
                }
                if(isset($_SESSION['empty_fields_reg']))
                {
                    echo '<p>Введены не все значения</p>';
                }
                if(isset($_SESSION['user_exists']))
                {
                    echo '<p>Пользователь с таким именем уже существует</p>';
                }
            ?>
            <button type="submit">Зарегистрироваться</button>
        </form>
    </div>
    
    
</body>
</html>
