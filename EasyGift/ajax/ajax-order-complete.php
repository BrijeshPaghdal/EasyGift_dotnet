<?php
  session_start();
  require_once('../config.php');

  $o_c_id = $_POST['o_c_id'];
  $status = $_POST['status'];

  $tempUrl = $url . 'api/EasyGift/OrderComplete/'.$o_c_id;
  $data = array( 'OrderCompleteStatus' => $status);
  $data = json_encode($data);
  $response = patchData($tempUrl,$data);

  if($response['statusCode']==200 && count($response['result'])>0)
  {
    echo "Order Complete";
  }
  else {
    echo "An error Occured";
  }
  
?>
