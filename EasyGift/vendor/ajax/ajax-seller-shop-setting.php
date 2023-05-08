<?php
if(isset($_POST['Shop_Name']))
{
	include 'check-seller.php';

	$shop_id = $_SESSION['Shop_Id'];
	$seller_id = $_SESSION['Seller_Id'];

	$shop_name = $_POST['Shop_Name'];
	$gst_no = $_POST['GST_NO'];
	$latitude = $_POST['lat'];
	$longitude = $_POST['long'];

	$address = $_POST['Shop_Addr'];
	$city = $_POST['City'];
	$state = $_POST['State'];
	$country = $_POST['Country'];
	$pincode = $_POST['Pincode'];

	$data = array(  'ShopName' => $shop_name,
	'GSTNo'=>$gst_no,
	'Latitude'=>$latitude,
	'Longitude'=>$longitude);

	$data = json_encode($data);
	$tempUrl = $url.'api/EasyGift/Shop/'.$shop_id;
	$response = patchData($tempUrl,$data);
	if ($response['statusCode']==200 && count($response['result']) > 0) {

		$tempUrl = $url.'api/EasyGift/Cities?filter=CityName.Equals("'.$city.'")';
		$response = fetchRequest($tempUrl);
		if($response['statusCode']==200 && count($response['result'])>0)
		{
            $city_id = $response['result'][0]['id'];
        }

		$tempUrl = $url.'api/EasyGift/Address?filter=ShopId='.$shop_id;
		$response = fetchRequest($tempUrl);
		if($response['statusCode']==200 && count($response['result'])>0)
		{
            $address_id = $response['result'][0]['id'];

			$data = array(  'ShopAddress' => $address,
			'PinCode'=>$pincode,
			'CityId'=>$city_id);
			
			$data = json_encode($data);
			$tempUrl = $url.'api/EasyGift/Address/'.$address_id;
			$response = patchData($tempUrl,$data);
			if ($response['statusCode']==200 && count($response['result']) > 0) {
				echo "1";
			}
			else
			{
				echo "0";
			}
		}
    }
	else
	{
		echo "0";
	}
    
}
?>