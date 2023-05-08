<?php
if(isset($_POST['Seller_Name']))
{
	include 'check-seller.php';

	$shop_id = $_SESSION['Shop_Id'];
	$seller_id = $_SESSION['Seller_Id'];

	$seller_name = $_POST['Seller_Name'];
	$seller_l_name = $_POST['Seller_L_Name'];
    $seller_phone_no = $_POST['Seller_Phon_no'];
    $seller_pancard_no = $_POST['seller_pancard_no'];

    $email_id = $_POST['Seller_Email_Id'];

    if(isset($_FILES['image']) && $_FILES['image']['name'] != '')
    {
        $tempUrl = $url.'api/EasyGift/Seller/'.$seller_id.'?columns=SellerImage';
        $response = fetchRequest($tempUrl);
        if($response['statusCode']==200 && count($response['result'])>0)
        {
            $image_name  = $response['result']['SellerImage'];  
            $filePath = "../seller-images/".$image_name;
            if (file_exists($filePath)) 
            {
                unlink($filePath);
            }
        }
        else
        {
        	echo "0";
        }

        $file_name = $_FILES['image']['name'];
        $file_tmp_name = $_FILES['image']['tmp_name'];
        $extension = pathinfo($file_name,PATHINFO_EXTENSION);
        $valid_extension = array("jpg", "jpeg" , "png");
            
        if(in_array($extension, $valid_extension))
        {
            if( $_FILES['image']['size']<200000 )
            {
                $new_file_name = $_SESSION['Seller_Id']."-".date("d-m-y-h-m-s")."-".rand(1,10000).$seller_name;
            
                move_uploaded_file($file_tmp_name, "../seller-images/".$new_file_name);
                
                $data = array('SellerImage' => $new_file_name);
                $data = json_encode($data);
                $tempUrl = $url.'api/EasyGift/Seller/'.$seller_id;
                $response = patchData($tempUrl,$data);
                if ($response['statusCode']==200 && count($response['result']) > 0) {

                	$_SESSION['Seller_Image'] = $new_file_name;
                    $temp = 1;
                }
                else
                {
                    echo "0";
                }
            }
            else
            {
            	echo "1";
            }
        }
        else
        {
        	echo "2";
        }
    }

    $tempUrl = $url.'api/EasyGift/Seller/'.$seller_id.'?columns=SellerLoginId';
    $response = fetchRequest($tempUrl);
    if($response['statusCode']==200 && count($response['result'])>0)
    {
    	$s_login_id = $response['result']['SellerLoginId'];

        $data = array('EmailId' => $email_id);
        $data = json_encode($data);
        $tempUrl = $url.'api/EasyGift/SellerLogin/'.$s_login_id;
        $response = patchData($tempUrl,$data);
        if ($response['statusCode']==200 && count($response['result']) > 0) {
    
	    	setcookie("vendor_login", "", time() - 3600, "/");
			setcookie("vendor_password", "", time() - 3600, "/");


            $data = array(  'SellerName' => $seller_name,
                            'SellerLastName'=>$seller_l_name,
                            'SellerPhoneNo'=>$seller_phone_no,
                            'SellerPancardNo'=>$seller_pancard_no);
            $data = json_encode($data);
            $tempUrl = $url.'api/EasyGift/Seller/'.$seller_id;
            $response = patchData($tempUrl,$data);
            if ($response['statusCode']==200 && count($response['result']) > 0) {
		    	$_SESSION['Seller_Name'] = $seller_name;
				echo "3";
		    }
		    else
		    {
		        echo "0";
		    }
	    }
	    else
	    {
	        echo "0";
	    }    	
    }
    else
    {
    	echo "0";
    }
}
else
{
	header("Location:404.php");
}