<?php
	include 'check-seller.php';

	$bank_name = $_POST['B_Name'];
    $bank_branch = $_POST['Branch'];

    $tempUrl = $url.'api/EasyGift/BankDetails/GetDistinctBankDetails?BankName='.$bank_name.'&BankBranch='.$bank_branch;
    $response = fetchRequest($tempUrl);
  
    if($response['statusCode']==200 && count($response['result'])>0)
    {
        foreach($response['result'] as $row)
        {
            $output = '<textarea rows="4" class="form-control no-resize" name ="bank_address" placeholder="Address Line 1" disabled>'.$row['BankAddress'].'</textarea>';   
        }
    }
    echo $output;

?>