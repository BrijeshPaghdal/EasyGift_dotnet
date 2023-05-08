<?php
  include 'config.php';
  session_start();
  
  $session_id = session_id();
  $time = time();
  $time_out_in_sec = 120;
  $time_out = $time - $time_out_in_sec;
  $data = array('session_id' => $session_id, 'session_time' => $time);
	$data = json_encode($data);
	$tempUrl = $url.'api/EasyGift/UserOnline/CheckUserIsOnline';
	$response = addData($tempUrl,$data);
?>
