<?php
if (isset($_POST['SugggestFor'])) {

    require_once('./check-admin.php');

    $sugg_for =  $_POST['SugggestFor'];
    $gender =  $_POST['gender'];
    $min_age =  $_POST['minage'];
    $max_age =  $_POST['maxage'];

    $tempUrl = $url.'api/EasyGift/Suggestion?columns=Id&filter=SuggestedFor.Contains("'.$sugg_for.'")';
    $response = fetchRequest($tempUrl);
    if($response['statusCode']==404)
    {
        $data = array('SuggestedFor' => $sugg_for,
                        'Gender' => $gender,
                        'MinAge' => $min_age,
                        'MaxAge' => $max_age);
        $data = json_encode($data);
        $tempUrl = $url . 'api/EasyGift/Suggestion';
        $response = addData($tempUrl,$data);
        if ($response['statusCode'] == 201) {
            echo "success";
        } else {
            echo "2";
        }
    } else {
        echo "1";
    }
} else {
    header("Location:404.php");
}
