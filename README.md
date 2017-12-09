# MyFrameCore
中小型项目框架搭建 

包含功能：权限功能管理

包含技术：Asp.net Core+mysql+redis+SqlSugar/Dapper+WebApi+Jeui

企业级用户密码设计：用户表每个用户存放一个自己的随机数，并用AES对称加密。最终密码字段存放的数据—— AES解密后的随机数与真实输入密码合并后的字符串再通过SHA1给予不对称加密


WebApi安全验证方式：通行口令+签名
具体实现思路：访问api之前添加标题头，包括口令、标准时间、唯一标识、签名，其中签名=md5(标准时间+唯一标识+口令+传入参数)。传入参数以前面的唯一标识作为键保存在redis中。api这边通过读取redis获取传入参数，再获取标题头，然后重新生成签名，如果标题头的旧签名和新签名相等则没问题，反之传入参数被篡改，而且用一次就不能再用，相当安全。如果没有传入参数，还可以用标题头的标准时间和当前时间做减法来判断当前url是否过期。
