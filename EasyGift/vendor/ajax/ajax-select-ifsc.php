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
            $output = '<input type="text" class="form-control" name="ifsc_code" value="'.$row['BankIFSC'].'" 
            onkeydown="return false;">';
        }
    }
    echo $output;
?>