<?php
  require_once('../config.php');
  session_start();
  $city =  $_POST['city'];
  $latitude =  $_POST['lat'];
  $longitude =  $_POST['long'];

  $_SESSION['city'] = $city;
  $_SESSION['latitude'] = $latitude;
  $_SESSION['longitude'] = $longitude;


?>
