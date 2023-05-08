<?php
    include 'check-seller.php';
    
    $output = "<select>";
    
    $bank_name = $_POST['B_NAME'];
    $bank_state = $_POST['State'];
    $bank_city = $_POST['City'];

    $tempUrl = $url.'api/EasyGift/BankDetails/GetDistinctBankBranch?BankState='.$bank_state.'&BankCity='.$bank_city.'&BankName='.$bank_name;
    $response = fetchRequest($tempUrl);
  
    if($response['statusCode']==200 && count($response['result'])>0)
    {
        $output .= "<option selected disabled value=''>Bank Branch</option>";
        foreach($response['result'] as $row)
        {
            $output .= "<option>" . $row['BankBranch'] . "</option>";
        }
    }
    $output .= "</select>";
    echo $output;
?>