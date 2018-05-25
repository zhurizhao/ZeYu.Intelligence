 

 

$(document).ready(function () {
    initMap();


    getStation();
    setAlarm();
})

function setAlarm() {
    var myIcon = new BMap.Icon("/images/011k.gif", new BMap.Size(50, 40));
    var marker = new BMap.Marker(new BMap.Point(119.617001, 29.085838), { icon: myIcon });  
    marker.setLabel(label);
    content = "编号:<b>800006786</b> 车辆人员进入非正常活动区域。";
    addMouseOverHandler(content, marker);
    addMouseClickHandler(content, marker);
    map.addOverlay(marker);   
}

function getStation() {

    $.get("/api/Terminal?pageSize=1000", function (jResult) {
        
        if (jResult.hasOwnProperty("Body") && jResult.Body.Total > 0) {
            
            var list = jResult.Body.List;
            for (var i = 0; i < list.length; i++) {
                
                var item = list[i];
                var BDLocation = item.BDLocation.split(',');
                var point = new BMap.Point(BDLocation[0], BDLocation[1]);
               
                var addSpanId = "addr_" + i;
                var content = "<b> ID:" + item.ID + "</b> <hr/>" + item.Address;
                content += "<br/><b>位置：</b><em><span id='" + addSpanId + "'><img width='32' height='32' src='http://www.lydong.com/images/jdt.gif' class='load' onload='getAddress(" + addSpanId + "," + BDLocation[0] + "," + BDLocation[1] + ")' /></span></em>";
                var marker = new BMap.Marker(point);
                marker.setLabel(label);
                addMouseOverHandler(content, marker);
                addMouseClickHandler(content, marker);
                map.addOverlay(marker);
            }

        }
    });
}

