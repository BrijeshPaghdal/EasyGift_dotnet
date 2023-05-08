<?php
	include 'check-seller.php';
	$shop_id = $_SESSION['Shop_Id'];

	$days = 7;
	$tempUrl = $url.'api/EasyGift/Shop/'.$shop_id.'/GetTotalProductSoldforChart?limit='.$days;
	$response = fetchRequest($tempUrl);
	$output = "";
	$i = 1;
	if($response['statusCode']==200)
	{
	  foreach($response['result'] as $row)
	  {
		$output .= $row['ProductName'];
		if ($i >= count($response['result'])) {
				break;
			  } else {
				$output .= ",";
				$i++;
			  }
	  }
	}
	elseif($response['statusCode']==404)
	{
	  echo 0;
	}
	  echo $output;
?>