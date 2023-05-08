<?php
  session_start();
  require_once("../config.php");

  $cust_id = $_SESSION['user_id'];

  $ord_id = $_POST['ord_id'];
  $rating = $_POST['rating'];
  $review = $_POST['review'];
  $date = new DateTime();
  $dateString = $date->format('Y-m-d H:i:s');
  $tempUrl = $url . 'api/EasyGift/Review';
  $data = array('OrderId' => $ord_id, 'Rating' => $rating, 'ReviewDetail'=>$review , 'ReviewDate'=>$dateString);
  $data = json_encode($data);
  $response = addData($tempUrl,$data);
  if ($response['statusCode'] == 201 && count($response['result'])>0) {
      echo "success";
  }
  else {
    echo "Error Occured";
  }
?>
