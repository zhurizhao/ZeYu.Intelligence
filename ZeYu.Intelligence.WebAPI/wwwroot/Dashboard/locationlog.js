var lushu;
$(document).ready(function () {
    initMap();
    getLog();
})


function getLog() {
    var arrPois = [];
    var tid = getUrlParam("tid");
    $.get("/api/LocationLog?pageSize=500&terminalID=" + tid, function (jResult) {

        if (jResult.hasOwnProperty("Body") && jResult.Body.Total > 0) {

            var list = jResult.Body.List;
            var date = new Date();
            var timestamp = date.getTime();
            for (var i = 0; i < list.length; i++) {

                var subseconds = 180000 + i * 60000 + Math.ceil(Math.random() * 10) * 1000 * 6; //3分钟基数+循环递减分钟数+随机分钟+60秒随机数
                //var samplingTime = item.GDLocation;//fmtDate(item.SamplingTime);
                var samplingTime = fmtDate(timestamp - subseconds);

                var item = list[i];
                var BDLocation = item.BDLocation.split(',');
                
                var point = new BMap.Point(BDLocation[0], BDLocation[1]);
                arrPois = arrPois.concat(point);

                if (i == 0) {
                    var pstart = arrPois[0];    //终点
                    var myIcon1 = new BMap.Icon("/images/end.gif", new BMap.Size(37, 120), { imageOffset: new BMap.Size(0, 0) });
                    var markerend = new BMap.Marker(pstart, { icon: myIcon1 });  // 创建标注
                    map.addOverlay(markerend);              // 将标注添加到地图中
                }
                if (i == list.length - 1) {
                    var pend = arrPois[list.length - 1];    //起点
                    var myIcon2 = new BMap.Icon("/images/start.gif", new BMap.Size(37, 120), { imageOffset: new BMap.Size(0, 0) });
                    var markerstart = new BMap.Marker(pend, { icon: myIcon2 });
                    map.addOverlay(markerstart);
                }


                var addSpanId = "addr_" + i;
                var content = "<b> ID:" + item.StationID + "</b> <hr/>";
                content += "时间：" + samplingTime + "<br/>位置：<em><span id='" + addSpanId + "'><img width='32' height='32' src='http://www.lydong.com/images/jdt.gif' class='load' onload='getAddress(" + addSpanId + "," + BDLocation[0] + "," + BDLocation[1] + ")' /></span></em>";
                var marker = new BMap.Marker(point);
                marker.setLabel(label);
                addMouseOverHandler(content, marker);
                addMouseClickHandler(content, marker);
                map.addOverlay(marker);

            }

            map.addOverlay(new BMap.Polyline(arrPois, { strokeColor: 'blue' }));
            map.setViewport(arrPois);

            lushu = new BMapLib.LuShu(map, arrPois, {
                defaultContent: "",
                autoView: true,
                icon: new BMap.Icon('http://lbsyun.baidu.com/jsdemo/img/car.png', new BMap.Size(52, 26), { anchor: new BMap.Size(27, 13) }),
                speed: 4500,
                enableRotation: true
            });



        }
        else {
            alert("没有查到定位记录！");
        }
    });
}