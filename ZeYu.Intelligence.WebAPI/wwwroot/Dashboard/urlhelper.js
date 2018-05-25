 function getUrlParam(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
            var r = window.location.search.substr(1).match(reg);  //匹配目标参数
            if (r != null) return unescape(r[2]); return null; //返回参数值
}

 function fmtDate(obj) {
     var date = new Date(obj);
     var y = 1900 + date.getYear();
     var M = "0" + (date.getMonth() + 1);
     var d = "0" + date.getDate();
     var h = date.getHours() + ':';
     var m = date.getMinutes() + ':';
     var s = date.getSeconds(); 
     return y + "-" + M.substring(M.length - 2, M.length) + "-" + d.substring(d.length - 2, d.length)+" "+h+m+s;
 }


 