<?php
if(isset($_POST['B_ACC_NO']))
{
	include 'check-seller.php';
	
	$seller_id = $_SESSION['Seller_Id'];

	$b_country = $_POST['bank_country'];
    $b_state = $_POST['bank_state'];
    $b_city = $_POST['bank_city'];
    $b_name = $_POST['bank_name'];
    $b_branch = $_POST['bank_branch'];
	$ifsc_code = $_POST['ifsc_code'];    

    $b_acc_no = $_POST['B_ACC_NO'];

    $tempUrl = $url.'api/EasyGift/BankDetails?columns=Id&filter=BankCountry.Equals("'.$b_country.'") and BankState.Equals("'.$b_state.'") and BankCity.Equals("'.$b_city.'")and BankName.Equals("'.$b_name.'") and BankBranch.Equals("'.$b_branch.'") and BankIFSC.Equals("'.$ifsc_code.'")';
    $response = fetchRequest($tempUrl);
    if($response['statusCode']==200 && count($response['result'])>0)
    {
        $row = $response['result'][0];
        $bank_id = $row['Id'];
    } 

    $tempUrl = $url.'api/EasyGift/SellerBankDetails?columns=Id&filter=SellerId='.$seller_id;
    $response = fetchRequest($tempUrl);

    if($response['statusCode']==200 && count($response['result'])>0)
    {
        $seller_bank_id = $response['result'][0]['Id'];
    
        $data = array(  'BankId' => $bank_id,
        'BankAccountNo'=>$b_acc_no);

        $data = json_encode($data);
        $tempUrl = $url.'api/EasyGift/SellerBankDetails/'.$seller_bank_id;
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
?>