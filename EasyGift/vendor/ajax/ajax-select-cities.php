<?php
    include 'check-seller.php';
    
    $state_name = $_POST['State'];

    $tempUrl = $url.'api/EasyGift/States?filter=StateName.Equals("'.$state_name.'")';
    $response = fetchRequest($tempUrl);
   
    if($response['statusCode']==200 && count($response['result'])>0)
    {
        $state_id = $response['result'][0]['id'];
    }

    $output = "<select>";

    $tempUrl = $url.'api/EasyGift/Cities?filter=StateId='.$state_id;
    $response = fetchRequest($tempUrl);

    if($response['statusCode']==200 && count($response['result'])>0)
    {
        $output .= "<option selected disabled value=''>State</option>";
        foreach($response['result'] as $row)
        {
            $output .= "<option>" . $row['cityName'] . "</option>";
        }
    }
    $output .= "</select>";
    echo $output;
?>