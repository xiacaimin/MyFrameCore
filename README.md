# MyFrameCore
中小型项目框架搭建 

包含功能：权限功能管理

包含技术：Asp.net Core+mysql+redis+SqlSugar/Dapper+WebApi+Jeui

企业级用户密码设计：用户表每个用户存放一个自己的随机数，并用AES对称加密。最终密码字段存放的数据—— AES解密后的随机数与真实输入密码合并后的字符串再通过SHA1给予不对称加密
PS:AES,高级加密标准。在密码学中又称Rijndael加密法，是美国联邦政府采用的一种区块加密标准。这个标准用来替代原先的DES，已经被多方分析且广为全世界所使用。经过五年的甄选流程，高级加密标准由美国国家标准与技术研究院（NIST）于2001年11月26日发布于FIPS PUB 197，并在2002年5月26日成为有效的标准。2006年，高级加密标准已然成为对称密钥加密中最流行的算法之一。
