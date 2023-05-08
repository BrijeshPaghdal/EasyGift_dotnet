<?php
  include 'check-admin.php';
  function deleteSubCate($url,$sugg_id)
  {
    $tempUrl = $url.'api/EasyGift/Suggestion/'.$sugg_id;
    $response = deleteRequest($tempUrl);
    if ($response['statusCode'] == 200) {
        echo "1";
    } else {
        echo "3";
    }
  }

    if(isset($_POST['sugg_id']))
    {
        $sugg_id = $_POST['sugg_id'];
        deleteSubCate($url,$sugg_id);
    }
    else {
      echo "2";
    }
?>
