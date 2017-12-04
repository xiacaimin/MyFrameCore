/*
Navicat MySQL Data Transfer

Source Server         : 60.205.187.235_3306
Source Server Version : 50556
Source Host           : 60.205.187.235:3306
Source Database       : myframedata

Target Server Type    : MYSQL
Target Server Version : 50556
File Encoding         : 65001

Date: 2017-11-22 10:31:17
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for sys_button
-- ----------------------------
DROP TABLE IF EXISTS `sys_button`;
CREATE TABLE `sys_button` (
  `KeyId` varchar(36) NOT NULL,
  `FullName` varchar(50) DEFAULT NULL,
  `Icon` varchar(100) DEFAULT NULL,
  `ButtonEvent` varchar(20) DEFAULT NULL,
  `Description` varchar(150) DEFAULT NULL,
  `SortNum` int(11) DEFAULT NULL,
  `IsDeleted` tinyint(4) DEFAULT NULL,
  `CreateDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`KeyId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of sys_button
-- ----------------------------
INSERT INTO `sys_button` VALUES ('0AF962FC-B3FA-4CE0-B04B-988F13A501E4', '编辑', 'aweicon aweicon-edit', 'Edit()', '编辑按钮', '1', '0', '2017-10-19 17:08:47');
INSERT INTO `sys_button` VALUES ('41479DCB-7F70-42B1-8954-8214809ADB56', '查询', 'aweicon aweicon-search', 'Search()', '查询按钮', '1', '0', '2017-10-19 17:08:47');
INSERT INTO `sys_button` VALUES ('4EB1E180-5EFC-4F36-843E-76C12357EFD9', '批量删除', 'aweicon aweicon-times-circle', 'BatchDelete()', '批量删除按钮', '1', '0', '2017-10-19 17:08:47');
INSERT INTO `sys_button` VALUES ('5c45954e-6674-4d13-89e7-1d08d47342e3', '发', null, null, null, null, '1', '2017-08-18 11:44:41');
INSERT INTO `sys_button` VALUES ('c4af4983-090f-4bc2-a85b-511aa7b4e9c6', 'test', 'dfgadf', 'Add2()', 'sd', '5', '1', '2017-08-11 16:29:05');
INSERT INTO `sys_button` VALUES ('C9DC049E-ADB8-438B-8C6F-1805E62430A3', '新增', 'aweicon aweicon-plus', 'Add()', '新增按钮', '1', '0', '2017-10-19 17:08:47');
INSERT INTO `sys_button` VALUES ('D6FCB68F-763C-4D12-9C1B-0D20AD47AA0A', '删除', 'aweicon aweicon-remove', 'Delete()', '删除按钮', '1', '0', '2017-10-19 17:08:47');

-- ----------------------------
-- Table structure for sys_dictionary
-- ----------------------------
DROP TABLE IF EXISTS `sys_dictionary`;
CREATE TABLE `sys_dictionary` (
  `KeyId` varchar(36) NOT NULL,
  `ParentId` varchar(36) DEFAULT NULL,
  `FullName` varchar(50) DEFAULT NULL,
  `Img` varchar(100) DEFAULT NULL,
  `Url` varchar(100) DEFAULT NULL,
  `Description` varchar(150) DEFAULT NULL,
  `SortNum` int(11) DEFAULT NULL,
  `IsDeleted` tinyint(4) DEFAULT NULL,
  `PIds` longtext,
  `CreateDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`KeyId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of sys_dictionary
-- ----------------------------
INSERT INTO `sys_dictionary` VALUES ('041D6541-9D43-4239-AD49-9AE614160B0F', '94BDDC0D-440C-4C27-B0F6-66DEE6EE467D', '总经理', null, null, null, '3', '0', '94BDDC0D-440C-4C27-B0F6-66DEE6EE467D|', '2017-10-19 17:08:48');
INSERT INTO `sys_dictionary` VALUES ('0F668E37-5545-4F46-8F21-DE17EB25013F', '89720187-7730-4096-9163-4D47709FC278', '博士', null, null, null, '7', '0', '89720187-7730-4096-9163-4D47709FC278|', '2017-10-19 17:08:48');
INSERT INTO `sys_dictionary` VALUES ('2D6B1AA3-5C31-4ABA-AE52-1896732B6FCD', '89720187-7730-4096-9163-4D47709FC278', '本科', null, null, null, '5', '0', '89720187-7730-4096-9163-4D47709FC278|', '2017-10-19 17:08:48');
INSERT INTO `sys_dictionary` VALUES ('6276A1B1-8493-4524-B8B1-51C1C5915138', '89720187-7730-4096-9163-4D47709FC278', '初中', null, null, null, '2', '0', '89720187-7730-4096-9163-4D47709FC278|', '2017-10-19 17:08:48');
INSERT INTO `sys_dictionary` VALUES ('754B27E5-BA57-485A-A466-C500678F7EEA', '94BDDC0D-440C-4C27-B0F6-66DEE6EE467D', '部门经理', null, null, null, '2', '0', '94BDDC0D-440C-4C27-B0F6-66DEE6EE467D|', '2017-10-19 17:08:48');
INSERT INTO `sys_dictionary` VALUES ('7CC74A7F-9DDE-499B-BD26-F1D0140BD92B', '89720187-7730-4096-9163-4D47709FC278', '专科', null, null, null, '4', '0', '89720187-7730-4096-9163-4D47709FC278|', '2017-10-19 17:08:48');
INSERT INTO `sys_dictionary` VALUES ('89720187-7730-4096-9163-4D47709FC278', null, '学历', null, null, null, null, '0', null, '2017-10-19 17:08:48');
INSERT INTO `sys_dictionary` VALUES ('94BDDC0D-440C-4C27-B0F6-66DEE6EE467D', null, '职位', null, null, null, null, '0', null, '2017-10-19 17:08:48');
INSERT INTO `sys_dictionary` VALUES ('A4131D82-C9D5-4F77-B127-5B0B4B9BAF2C', '89720187-7730-4096-9163-4D47709FC278', '小学', null, null, null, '1', '0', '89720187-7730-4096-9163-4D47709FC278|', '2017-10-19 17:08:48');
INSERT INTO `sys_dictionary` VALUES ('A9821D1E-5433-43BD-9747-6331407B50D6', '94BDDC0D-440C-4C27-B0F6-66DEE6EE467D', '普通员工', null, null, null, '1', '0', '94BDDC0D-440C-4C27-B0F6-66DEE6EE467D|', '2017-10-19 17:08:48');
INSERT INTO `sys_dictionary` VALUES ('B71E4C03-C3EF-4F38-B860-85633AD00EA4', '89720187-7730-4096-9163-4D47709FC278', '研究生', null, null, null, '6', '0', '89720187-7730-4096-9163-4D47709FC278|', '2017-10-19 17:08:48');
INSERT INTO `sys_dictionary` VALUES ('CC718106-5972-4ED3-A8E3-4B38829629D8', '89720187-7730-4096-9163-4D47709FC278', '中专/高中', null, null, null, '3', '0', '89720187-7730-4096-9163-4D47709FC278|', '2017-10-19 17:08:48');
INSERT INTO `sys_dictionary` VALUES ('E1A8BD62-7E0A-4C6C-9B3F-065D7D29609A', '94BDDC0D-440C-4C27-B0F6-66DEE6EE467D', '董事长', null, null, null, '4', '0', '94BDDC0D-440C-4C27-B0F6-66DEE6EE467D|', '2017-10-19 17:08:48');

-- ----------------------------
-- Table structure for sys_errorlog
-- ----------------------------
DROP TABLE IF EXISTS `sys_errorlog`;
CREATE TABLE `sys_errorlog` (
  `KeyId` int(11) NOT NULL,
  `Title` varchar(255) DEFAULT NULL,
  `ErrorMsg` varchar(255) DEFAULT NULL,
  `CreateTime` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`KeyId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of sys_errorlog
-- ----------------------------

-- ----------------------------
-- Table structure for sys_menu
-- ----------------------------
DROP TABLE IF EXISTS `sys_menu`;
CREATE TABLE `sys_menu` (
  `KeyId` varchar(36) NOT NULL,
  `ParentId` varchar(36) DEFAULT NULL,
  `FullName` varchar(50) DEFAULT NULL,
  `Icon` varchar(100) DEFAULT NULL,
  `NavigateUrl` varchar(100) DEFAULT NULL,
  `IsRoot` tinyint(4) DEFAULT NULL,
  `Description` varchar(150) DEFAULT NULL,
  `SortNum` int(11) DEFAULT NULL,
  `IsDeleted` tinyint(4) DEFAULT NULL,
  `CreateDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`KeyId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of sys_menu
-- ----------------------------
INSERT INTO `sys_menu` VALUES ('041D93A6-18F9-40C4-8698-33E6F63F700C', null, '仓库管理', null, null, '1', '根菜单', '4', '0', '2017-10-19 17:08:48');
INSERT INTO `sys_menu` VALUES ('07335167-2354-4DC3-8B96-7ED5C35962D2', 'CD62477E-04AC-4014-A391-88A9BB0675D0', '系统管理', null, null, '0', '导航菜单划分类', '1', '0', '2017-10-19 17:08:48');
INSERT INTO `sys_menu` VALUES ('22E74F20-0C6D-4512-B173-122605AFD752', 'C3F5D174-BF6A-4AF3-A339-30BDA7D0588B', '产品管理', null, '/Admin/Product/Index', '0', '导航菜单链接', '1', '0', '2017-10-19 17:08:48');
INSERT INTO `sys_menu` VALUES ('280133C1-AD8F-46C1-AFC5-A8E8784C2C10', null, '客户管理', null, null, '1', '根菜单', '5', '0', '2017-10-19 17:08:48');
INSERT INTO `sys_menu` VALUES ('2932CB88-97DF-423E-A4D7-D679CFFCD766', null, '产品管理', 'aweicon aweicon-empire', null, '1', '根菜单', '1', '0', '2017-10-19 17:08:48');
INSERT INTO `sys_menu` VALUES ('3016390b-e392-4dec-a1f4-5e61ee97ebb8', '5C1ACB56-DBE2-4BEA-A71F-28EACC45E3C9', '错误日志', null, '/Admin/Main/Error', '0', null, '4', '0', '2017-08-11 17:14:36');
INSERT INTO `sys_menu` VALUES ('4901CB3B-8158-4CBB-80EB-305D68154B22', '5C1ACB56-DBE2-4BEA-A71F-28EACC45E3C9', '菜单管理', null, '/Admin/Menu/Index', '0', '导航菜单链接', '2', '0', '2017-10-19 17:08:48');
INSERT INTO `sys_menu` VALUES ('5C1ACB56-DBE2-4BEA-A71F-28EACC45E3C9', 'CD62477E-04AC-4014-A391-88A9BB0675D0', '开发者管理', null, null, '0', '导航菜单划分类', '2', '0', '2017-10-19 17:08:48');
INSERT INTO `sys_menu` VALUES ('6AB06C4A-DB0A-4CF0-9502-2A87221754B5', '5C1ACB56-DBE2-4BEA-A71F-28EACC45E3C9', '按钮管理', null, '/Admin/Button/Index', '0', '导航菜单链接', '1', '0', '2017-10-19 17:08:48');
INSERT INTO `sys_menu` VALUES ('7A2719B7-61B8-407A-B81B-789902502B9C', null, '订单管理', null, null, '1', '根菜单', '2', '0', '2017-10-19 17:08:48');
INSERT INTO `sys_menu` VALUES ('8124e146-724d-413d-a8ac-2220a6a67d89', 'C3F5D174-BF6A-4AF3-A339-30BDA7D0588B', 'test', 'g', 's', '0', 'sss', '2', '1', '2017-08-10 17:10:43');
INSERT INTO `sys_menu` VALUES ('9FE4F4F1-9801-4A22-962C-DBEF331E0DC1', null, '财务管理', null, null, '1', '根菜单', '3', '0', '2017-10-19 17:08:48');
INSERT INTO `sys_menu` VALUES ('acc06b9a-b066-4ee7-bbb6-778a292fd302', '07335167-2354-4DC3-8B96-7ED5C35962D2', '用户管理', 'aweicon aweicon-user', '/Admin/User/Index', '0', null, '2', '0', '2017-08-17 11:49:03');
INSERT INTO `sys_menu` VALUES ('BD1FA6FC-B07E-46A9-93D5-03E9449C7234', '07335167-2354-4DC3-8B96-7ED5C35962D2', '权限管理', null, '/Admin/Permissions/Index', '0', '导航菜单链接', '2', '1', '2017-10-19 17:08:48');
INSERT INTO `sys_menu` VALUES ('C3F5D174-BF6A-4AF3-A339-30BDA7D0588B', '2932CB88-97DF-423E-A4D7-D679CFFCD766', '产品信息', null, null, '0', '导航菜单划分类', '1', '0', '2017-10-19 17:08:48');
INSERT INTO `sys_menu` VALUES ('CD62477E-04AC-4014-A391-88A9BB0675D0', null, '系统管理', 'aweicon aweicon-cog', null, '1', '根菜单', '6', '0', '2017-10-19 17:08:48');
INSERT INTO `sys_menu` VALUES ('DE4E3B4D-381D-4226-867E-1E43AD8E9250', '5C1ACB56-DBE2-4BEA-A71F-28EACC45E3C9', '图标浏览', null, '/Content/font-awesome/index.html', '0', '导航菜单链接', '3', '0', '2017-10-19 17:08:48');
INSERT INTO `sys_menu` VALUES ('F46A9D7C-31D9-4DF1-8F90-C84CF4FF94EF', '07335167-2354-4DC3-8B96-7ED5C35962D2', '角色管理', null, '/Admin/Role/Index', '0', '导航菜单链接', '1', '0', '2017-10-19 17:08:48');

-- ----------------------------
-- Table structure for sys_menubutton
-- ----------------------------
DROP TABLE IF EXISTS `sys_menubutton`;
CREATE TABLE `sys_menubutton` (
  `KeyId` varchar(36) NOT NULL,
  `MenuId` varchar(36) DEFAULT NULL,
  `ButtonId` varchar(36) DEFAULT NULL,
  `CreateDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`KeyId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of sys_menubutton
-- ----------------------------
INSERT INTO `sys_menubutton` VALUES ('067b7c8c-619b-49ee-b32e-4cfd70867841', '4901CB3B-8158-4CBB-80EB-305D68154B22', '4EB1E180-5EFC-4F36-843E-76C12357EFD9', '2017-08-11 11:25:43');
INSERT INTO `sys_menubutton` VALUES ('13e58f85-e2c9-49ea-8681-b679f1f40b52', 'acc06b9a-b066-4ee7-bbb6-778a292fd302', 'D6FCB68F-763C-4D12-9C1B-0D20AD47AA0A', '2017-08-17 11:49:44');
INSERT INTO `sys_menubutton` VALUES ('2f3ff817-b9fc-4da0-a53e-87e9400a1a19', '4901CB3B-8158-4CBB-80EB-305D68154B22', 'C9DC049E-ADB8-438B-8C6F-1805E62430A3', '2017-08-11 11:25:43');
INSERT INTO `sys_menubutton` VALUES ('45509aef-7dd0-4f88-81dd-20ba61a30ac6', '4901CB3B-8158-4CBB-80EB-305D68154B22', '41479DCB-7F70-42B1-8954-8214809ADB56', '2017-08-11 11:25:43');
INSERT INTO `sys_menubutton` VALUES ('485f443b-76b4-4ef4-86f3-8d1664338dd7', 'acc06b9a-b066-4ee7-bbb6-778a292fd302', '0AF962FC-B3FA-4CE0-B04B-988F13A501E4', '2017-08-17 11:49:44');
INSERT INTO `sys_menubutton` VALUES ('4b75de6e-a13f-46af-9bdb-176a0332fee9', '22E74F20-0C6D-4512-B173-122605AFD752', 'C9DC049E-ADB8-438B-8C6F-1805E62430A3', '2017-08-11 11:25:35');
INSERT INTO `sys_menubutton` VALUES ('593668a7-063e-4cfc-ac38-54d82fdae8cc', '22E74F20-0C6D-4512-B173-122605AFD752', '0AF962FC-B3FA-4CE0-B04B-988F13A501E4', '2017-08-11 11:25:35');
INSERT INTO `sys_menubutton` VALUES ('78bd7f5a-c098-4469-b217-69b55c1082f3', '22E74F20-0C6D-4512-B173-122605AFD752', '4EB1E180-5EFC-4F36-843E-76C12357EFD9', '2017-08-11 11:25:35');
INSERT INTO `sys_menubutton` VALUES ('7ba1167b-fb52-4ebc-9d0f-42ba8804c5ea', '22E74F20-0C6D-4512-B173-122605AFD752', '41479DCB-7F70-42B1-8954-8214809ADB56', '2017-08-11 11:25:35');
INSERT INTO `sys_menubutton` VALUES ('7cfb64cf-5afd-4eeb-ab50-62ba149bdecd', '6AB06C4A-DB0A-4CF0-9502-2A87221754B5', '0AF962FC-B3FA-4CE0-B04B-988F13A501E4', '2017-08-18 17:44:37');
INSERT INTO `sys_menubutton` VALUES ('9b78d0d3-1eed-4180-9854-34de928611a1', '22E74F20-0C6D-4512-B173-122605AFD752', 'D6FCB68F-763C-4D12-9C1B-0D20AD47AA0A', '2017-08-11 11:25:35');
INSERT INTO `sys_menubutton` VALUES ('a1b1fb43-45e0-4308-a4ed-a726b1645e86', 'acc06b9a-b066-4ee7-bbb6-778a292fd302', '41479DCB-7F70-42B1-8954-8214809ADB56', '2017-08-17 11:49:44');
INSERT INTO `sys_menubutton` VALUES ('a2a4385c-b14e-4e0b-acd5-2ae175eee72e', '4901CB3B-8158-4CBB-80EB-305D68154B22', 'D6FCB68F-763C-4D12-9C1B-0D20AD47AA0A', '2017-08-11 11:25:43');
INSERT INTO `sys_menubutton` VALUES ('ce1e5f7d-8ac8-4074-92bb-9e5181886d17', '4901CB3B-8158-4CBB-80EB-305D68154B22', '0AF962FC-B3FA-4CE0-B04B-988F13A501E4', '2017-08-11 11:25:43');
INSERT INTO `sys_menubutton` VALUES ('f179cca1-8855-4543-8ed9-65fa5d91d8a4', 'acc06b9a-b066-4ee7-bbb6-778a292fd302', 'C9DC049E-ADB8-438B-8C6F-1805E62430A3', '2017-08-17 11:49:44');

-- ----------------------------
-- Table structure for sys_organization
-- ----------------------------
DROP TABLE IF EXISTS `sys_organization`;
CREATE TABLE `sys_organization` (
  `KeyID` varchar(36) NOT NULL,
  `ParentId` varchar(36) DEFAULT NULL,
  `FullName` varchar(50) DEFAULT NULL,
  `Phone` varchar(20) DEFAULT NULL,
  `Description` varchar(150) DEFAULT NULL,
  `SortNum` int(11) DEFAULT NULL,
  `IsDeleted` tinyint(4) DEFAULT NULL,
  `PIds` longtext,
  `CreateDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`KeyID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of sys_organization
-- ----------------------------
INSERT INTO `sys_organization` VALUES ('7D1EAB93-A6A3-43FD-8F23-F6B06EE78BFC', 'AEE0CCBD-B8C1-45B7-B68F-B4E43A0D4964', '软件开发部', null, '从事软件研发工作', '1', '0', 'AEE0CCBD-B8C1-45B7-B68F-B4E43A0D4964|', '2017-10-19 17:08:48');
INSERT INTO `sys_organization` VALUES ('AEE0CCBD-B8C1-45B7-B68F-B4E43A0D4964', null, '公司', null, null, '0', '0', null, '2017-10-19 17:08:48');

-- ----------------------------
-- Table structure for sys_role
-- ----------------------------
DROP TABLE IF EXISTS `sys_role`;
CREATE TABLE `sys_role` (
  `KeyId` varchar(36) NOT NULL,
  `FullName` varchar(50) DEFAULT NULL,
  `Description` varchar(150) DEFAULT NULL,
  `IsDeleted` tinyint(4) DEFAULT NULL,
  `CreateDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`KeyId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of sys_role
-- ----------------------------
INSERT INTO `sys_role` VALUES ('0c2c4d34-173f-4f75-bbd9-4d97ba840e38', '售前角色', '售前', '1', '2017-08-08 17:37:48');
INSERT INTO `sys_role` VALUES ('1692398f-395a-4e70-9509-d78685f3d71c', '仓库管理', '仓库', '0', '2017-08-09 09:57:58');
INSERT INTO `sys_role` VALUES ('1a104f6c-41dd-45c5-b4b4-11fbd60e479d', '销售部', '销售', '1', '2017-08-08 16:18:32');
INSERT INTO `sys_role` VALUES ('1b933099-1193-438c-9568-c620ea433f1f', '财务', '收入支出', '0', '2017-08-11 10:02:39');
INSERT INTO `sys_role` VALUES ('39761F65-1FE2-493A-B32A-20E08DF9A22F', '开发者角色', '软件开发人员专有角色', '0', '2017-10-19 17:08:48');
INSERT INTO `sys_role` VALUES ('4249abef-d785-42b4-937e-055c82ca27d8', '人事角色', '人事管理', '1', '2017-08-08 17:41:31');
INSERT INTO `sys_role` VALUES ('513d6a28-c043-41bb-9b06-c7f032a2cbe3', '仓库角色', '管理库存', '1', '2017-08-08 17:37:08');
INSERT INTO `sys_role` VALUES ('6b119171-ff41-40a9-85b8-d826fc6c95c8', '客服角色', '客服在线', '1', '2017-08-08 17:43:05');
INSERT INTO `sys_role` VALUES ('ac791487-5a32-4eb9-ba0d-289ae90181c4', '财务角色', '财务', '1', '2017-08-08 17:33:33');

-- ----------------------------
-- Table structure for sys_rolemenu
-- ----------------------------
DROP TABLE IF EXISTS `sys_rolemenu`;
CREATE TABLE `sys_rolemenu` (
  `KeyId` varchar(36) NOT NULL,
  `RoleId` varchar(36) DEFAULT NULL,
  `MenuId` varchar(36) DEFAULT NULL,
  `CreateDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`KeyId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of sys_rolemenu
-- ----------------------------
INSERT INTO `sys_rolemenu` VALUES ('06228ae7-b67f-42a3-92ea-f450f881c35f', '39761F65-1FE2-493A-B32A-20E08DF9A22F', '3016390b-e392-4dec-a1f4-5e61ee97ebb8', '2017-08-17 11:50:14');
INSERT INTO `sys_rolemenu` VALUES ('0737360d-f9cd-44ce-98e0-1cb5ea05835a', '39761F65-1FE2-493A-B32A-20E08DF9A22F', '22E74F20-0C6D-4512-B173-122605AFD752', '2017-08-17 11:50:14');
INSERT INTO `sys_rolemenu` VALUES ('1d2d08c8-aed0-4dac-bf95-82b988713d37', '39761F65-1FE2-493A-B32A-20E08DF9A22F', '2932CB88-97DF-423E-A4D7-D679CFFCD766', '2017-08-17 11:50:14');
INSERT INTO `sys_rolemenu` VALUES ('2594033b-18b7-4244-8737-9500311c2f7f', '1b933099-1193-438c-9568-c620ea433f1f', '7A2719B7-61B8-407A-B81B-789902502B9C', '2017-08-17 16:59:22');
INSERT INTO `sys_rolemenu` VALUES ('27772171-8195-445c-a012-34aea6e50163', '1b933099-1193-438c-9568-c620ea433f1f', 'F46A9D7C-31D9-4DF1-8F90-C84CF4FF94EF', '2017-08-17 16:59:22');
INSERT INTO `sys_rolemenu` VALUES ('29d5dff2-7426-40fc-a9c9-0b44eb2943f8', '1b933099-1193-438c-9568-c620ea433f1f', '22E74F20-0C6D-4512-B173-122605AFD752', '2017-08-17 16:59:22');
INSERT INTO `sys_rolemenu` VALUES ('36fa6de8-aedc-4e97-9866-bb42ce3b821f', '1692398f-395a-4e70-9509-d78685f3d71c', '6AB06C4A-DB0A-4CF0-9502-2A87221754B5', '2017-08-10 14:40:37');
INSERT INTO `sys_rolemenu` VALUES ('39ccb1f7-3271-484c-ba7f-beff188c3c96', '1b933099-1193-438c-9568-c620ea433f1f', '280133C1-AD8F-46C1-AFC5-A8E8784C2C10', '2017-08-17 16:59:22');
INSERT INTO `sys_rolemenu` VALUES ('3ebfbe1d-c782-402e-8e73-f3e976a4547b', '1b933099-1193-438c-9568-c620ea433f1f', 'CD62477E-04AC-4014-A391-88A9BB0675D0', '2017-08-17 16:59:22');
INSERT INTO `sys_rolemenu` VALUES ('4df86f97-e6ad-4908-8fb6-4f516cbbca84', '1692398f-395a-4e70-9509-d78685f3d71c', '4901CB3B-8158-4CBB-80EB-305D68154B22', '2017-08-10 14:40:37');
INSERT INTO `sys_rolemenu` VALUES ('538c747b-3bd5-4af5-b50e-d22a66b14b9d', '39761F65-1FE2-493A-B32A-20E08DF9A22F', '5C1ACB56-DBE2-4BEA-A71F-28EACC45E3C9', '2017-08-17 11:50:14');
INSERT INTO `sys_rolemenu` VALUES ('57c08bc9-b18d-45e2-bdf6-39ddf8370a08', '39761F65-1FE2-493A-B32A-20E08DF9A22F', '6AB06C4A-DB0A-4CF0-9502-2A87221754B5', '2017-08-17 11:50:14');
INSERT INTO `sys_rolemenu` VALUES ('5adf21d2-d98d-4ed3-b2c0-df5b602644d2', '1692398f-395a-4e70-9509-d78685f3d71c', 'C3F5D174-BF6A-4AF3-A339-30BDA7D0588B', '2017-08-10 14:40:37');
INSERT INTO `sys_rolemenu` VALUES ('5d99c6ab-d046-452c-8e06-2076a1a21619', '1692398f-395a-4e70-9509-d78685f3d71c', '7A2719B7-61B8-407A-B81B-789902502B9C', '2017-08-10 14:40:37');
INSERT INTO `sys_rolemenu` VALUES ('63e3f15d-4400-4adf-b4fd-81fa0af4d479', '39761F65-1FE2-493A-B32A-20E08DF9A22F', '07335167-2354-4DC3-8B96-7ED5C35962D2', '2017-08-17 11:50:14');
INSERT INTO `sys_rolemenu` VALUES ('677bc755-cfd1-4c34-acaf-15133b8e22f6', '39761F65-1FE2-493A-B32A-20E08DF9A22F', 'F46A9D7C-31D9-4DF1-8F90-C84CF4FF94EF', '2017-08-17 11:50:14');
INSERT INTO `sys_rolemenu` VALUES ('6ef4ddf3-cd2b-423a-b3ce-97f9c6ea1f14', '1692398f-395a-4e70-9509-d78685f3d71c', '2932CB88-97DF-423E-A4D7-D679CFFCD766', '2017-08-10 14:40:37');
INSERT INTO `sys_rolemenu` VALUES ('70635169-9395-4674-a177-d599f7ca3146', '1692398f-395a-4e70-9509-d78685f3d71c', '5C1ACB56-DBE2-4BEA-A71F-28EACC45E3C9', '2017-08-10 14:40:37');
INSERT INTO `sys_rolemenu` VALUES ('71f56f0b-c999-4d35-a0b4-a6d84bc6dba7', '39761F65-1FE2-493A-B32A-20E08DF9A22F', 'CD62477E-04AC-4014-A391-88A9BB0675D0', '2017-08-17 11:50:14');
INSERT INTO `sys_rolemenu` VALUES ('797e79dd-f259-46eb-b08b-79142687afd7', '1692398f-395a-4e70-9509-d78685f3d71c', '9FE4F4F1-9801-4A22-962C-DBEF331E0DC1', '2017-08-10 14:40:37');
INSERT INTO `sys_rolemenu` VALUES ('7e61ef47-a525-4d58-89a0-307fed7f795a', '39761F65-1FE2-493A-B32A-20E08DF9A22F', '041D93A6-18F9-40C4-8698-33E6F63F700C', '2017-08-17 11:50:14');
INSERT INTO `sys_rolemenu` VALUES ('8aab9c14-af63-45fa-b83e-fdb66e7351f7', '1692398f-395a-4e70-9509-d78685f3d71c', 'F46A9D7C-31D9-4DF1-8F90-C84CF4FF94EF', '2017-08-10 14:40:37');
INSERT INTO `sys_rolemenu` VALUES ('8d296a72-f564-4582-85e6-d67c56277c62', '1b933099-1193-438c-9568-c620ea433f1f', '4901CB3B-8158-4CBB-80EB-305D68154B22', '2017-08-17 16:59:22');
INSERT INTO `sys_rolemenu` VALUES ('9a4e18c5-2259-4b4b-8600-8e975fe82e69', '1b933099-1193-438c-9568-c620ea433f1f', '2932CB88-97DF-423E-A4D7-D679CFFCD766', '2017-08-17 16:59:22');
INSERT INTO `sys_rolemenu` VALUES ('a06b84f0-0c50-4fb4-803b-166bb034fe75', '39761F65-1FE2-493A-B32A-20E08DF9A22F', '7A2719B7-61B8-407A-B81B-789902502B9C', '2017-08-17 11:50:14');
INSERT INTO `sys_rolemenu` VALUES ('aa96579f-fe50-4853-ad11-8a696859a268', '39761F65-1FE2-493A-B32A-20E08DF9A22F', '9FE4F4F1-9801-4A22-962C-DBEF331E0DC1', '2017-08-17 11:50:14');
INSERT INTO `sys_rolemenu` VALUES ('b036e4f7-284d-41ba-baf9-a0fc4acb8197', '1b933099-1193-438c-9568-c620ea433f1f', 'acc06b9a-b066-4ee7-bbb6-778a292fd302', '2017-08-17 16:59:22');
INSERT INTO `sys_rolemenu` VALUES ('b0c003b3-8724-4ec2-848a-ecb289304de4', '1b933099-1193-438c-9568-c620ea433f1f', '07335167-2354-4DC3-8B96-7ED5C35962D2', '2017-08-17 16:59:22');
INSERT INTO `sys_rolemenu` VALUES ('b3c13a68-9621-4008-ae38-f9661cde67fd', '39761F65-1FE2-493A-B32A-20E08DF9A22F', '4901CB3B-8158-4CBB-80EB-305D68154B22', '2017-08-17 11:50:14');
INSERT INTO `sys_rolemenu` VALUES ('b56d074e-e7a4-4f46-a4eb-00775c5142b5', '1692398f-395a-4e70-9509-d78685f3d71c', '22E74F20-0C6D-4512-B173-122605AFD752', '2017-08-10 14:40:37');
INSERT INTO `sys_rolemenu` VALUES ('be1e624e-42b9-49d7-bf63-184c1acfa3f3', '1692398f-395a-4e70-9509-d78685f3d71c', 'CD62477E-04AC-4014-A391-88A9BB0675D0', '2017-08-10 14:40:37');
INSERT INTO `sys_rolemenu` VALUES ('c2ed32d7-b46b-4ac4-a8de-22240b4d8034', '39761F65-1FE2-493A-B32A-20E08DF9A22F', 'acc06b9a-b066-4ee7-bbb6-778a292fd302', '2017-08-17 11:50:14');
INSERT INTO `sys_rolemenu` VALUES ('c882ba0c-5f3f-4b64-8c75-5b2e39609c85', '39761F65-1FE2-493A-B32A-20E08DF9A22F', 'C3F5D174-BF6A-4AF3-A339-30BDA7D0588B', '2017-08-17 11:50:14');
INSERT INTO `sys_rolemenu` VALUES ('d935d4f6-ad95-46c4-945b-a5c7ae97e6e6', '1692398f-395a-4e70-9509-d78685f3d71c', 'BD1FA6FC-B07E-46A9-93D5-03E9449C7234', '2017-08-10 14:40:37');
INSERT INTO `sys_rolemenu` VALUES ('d9b01a5c-ec71-4ba4-bb3b-b6da07384038', '39761F65-1FE2-493A-B32A-20E08DF9A22F', '280133C1-AD8F-46C1-AFC5-A8E8784C2C10', '2017-08-17 11:50:14');
INSERT INTO `sys_rolemenu` VALUES ('ddd53083-5f0e-4eb4-a2f4-371afbb2f877', '1b933099-1193-438c-9568-c620ea433f1f', '5C1ACB56-DBE2-4BEA-A71F-28EACC45E3C9', '2017-08-17 16:59:22');
INSERT INTO `sys_rolemenu` VALUES ('e980e401-911a-4f96-aba3-e26696c9b81a', '39761F65-1FE2-493A-B32A-20E08DF9A22F', 'DE4E3B4D-381D-4226-867E-1E43AD8E9250', '2017-08-17 11:50:14');
INSERT INTO `sys_rolemenu` VALUES ('eec10fc8-ae8f-456f-86f7-227ad9248de9', '1692398f-395a-4e70-9509-d78685f3d71c', 'DE4E3B4D-381D-4226-867E-1E43AD8E9250', '2017-08-10 14:40:37');
INSERT INTO `sys_rolemenu` VALUES ('f70b14fc-ed44-4575-a6b3-d9681702a30b', '1b933099-1193-438c-9568-c620ea433f1f', 'C3F5D174-BF6A-4AF3-A339-30BDA7D0588B', '2017-08-17 16:59:22');
INSERT INTO `sys_rolemenu` VALUES ('f9070411-f5db-4a92-9bfa-1ab775e6b49b', '1692398f-395a-4e70-9509-d78685f3d71c', '07335167-2354-4DC3-8B96-7ED5C35962D2', '2017-08-10 14:40:37');

-- ----------------------------
-- Table structure for sys_rolemenubutton
-- ----------------------------
DROP TABLE IF EXISTS `sys_rolemenubutton`;
CREATE TABLE `sys_rolemenubutton` (
  `KeyId` varchar(36) NOT NULL,
  `RoleId` varchar(36) DEFAULT NULL,
  `MenuId` varchar(36) DEFAULT NULL,
  `ButtonId` varchar(36) DEFAULT NULL,
  `CreateDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`KeyId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of sys_rolemenubutton
-- ----------------------------
INSERT INTO `sys_rolemenubutton` VALUES ('14a2ded8-629d-4d5f-9b45-a4690ae4ff25', '1b933099-1193-438c-9568-c620ea433f1f', '4901CB3B-8158-4CBB-80EB-305D68154B22', '4EB1E180-5EFC-4F36-843E-76C12357EFD9', '2017-08-17 16:59:22');
INSERT INTO `sys_rolemenubutton` VALUES ('42462e1c-2c7c-45fa-a33f-c36d86513f4f', '1b933099-1193-438c-9568-c620ea433f1f', 'acc06b9a-b066-4ee7-bbb6-778a292fd302', '0AF962FC-B3FA-4CE0-B04B-988F13A501E4', '2017-08-17 16:59:22');
INSERT INTO `sys_rolemenubutton` VALUES ('42a9d218-13aa-48a7-a5a3-6dcc64cf6194', '1692398f-395a-4e70-9509-d78685f3d71c', '22E74F20-0C6D-4512-B173-122605AFD752', '0AF962FC-B3FA-4CE0-B04B-988F13A501E4', '2017-08-10 14:40:37');
INSERT INTO `sys_rolemenubutton` VALUES ('4df8bb59-0a17-4bf5-88da-61dd3792d202', '1692398f-395a-4e70-9509-d78685f3d71c', '22E74F20-0C6D-4512-B173-122605AFD752', 'C9DC049E-ADB8-438B-8C6F-1805E62430A3', '2017-08-10 14:40:37');
INSERT INTO `sys_rolemenubutton` VALUES ('5a759250-5d73-4baf-b472-9199b765c7d5', '39761F65-1FE2-493A-B32A-20E08DF9A22F', '4901CB3B-8158-4CBB-80EB-305D68154B22', 'C9DC049E-ADB8-438B-8C6F-1805E62430A3', '2017-08-17 11:50:14');
INSERT INTO `sys_rolemenubutton` VALUES ('61d55546-bca0-41e4-8aea-102f78cb5301', '39761F65-1FE2-493A-B32A-20E08DF9A22F', '4901CB3B-8158-4CBB-80EB-305D68154B22', 'C9DC049E-ADB8-438B-8C6F-1805E62430A3', '2017-08-17 11:50:14');
INSERT INTO `sys_rolemenubutton` VALUES ('72036b85-7adc-4903-8add-6a5b57812595', '39761F65-1FE2-493A-B32A-20E08DF9A22F', 'acc06b9a-b066-4ee7-bbb6-778a292fd302', '0AF962FC-B3FA-4CE0-B04B-988F13A501E4', '2017-08-17 11:50:14');
INSERT INTO `sys_rolemenubutton` VALUES ('a6eac928-ec59-4711-b9f5-90a65f03d492', '1b933099-1193-438c-9568-c620ea433f1f', 'acc06b9a-b066-4ee7-bbb6-778a292fd302', 'D6FCB68F-763C-4D12-9C1B-0D20AD47AA0A', '2017-08-17 16:59:22');
INSERT INTO `sys_rolemenubutton` VALUES ('c2c5fdc5-35fd-449c-a544-a127b0af5177', '39761F65-1FE2-493A-B32A-20E08DF9A22F', 'acc06b9a-b066-4ee7-bbb6-778a292fd302', 'D6FCB68F-763C-4D12-9C1B-0D20AD47AA0A', '2017-08-17 11:50:14');
INSERT INTO `sys_rolemenubutton` VALUES ('c3469e0c-5bcf-4e84-8de9-7840be6b098d', '1692398f-395a-4e70-9509-d78685f3d71c', '22E74F20-0C6D-4512-B173-122605AFD752', '41479DCB-7F70-42B1-8954-8214809ADB56', '2017-08-10 14:40:37');
INSERT INTO `sys_rolemenubutton` VALUES ('c4b0f760-096c-4fca-b4e7-6d2dff7cc34c', '39761F65-1FE2-493A-B32A-20E08DF9A22F', 'acc06b9a-b066-4ee7-bbb6-778a292fd302', '0AF962FC-B3FA-4CE0-B04B-988F13A501E4', '2017-08-17 11:50:14');
INSERT INTO `sys_rolemenubutton` VALUES ('d03d1e64-b3ae-46af-81c5-04fffb29dcc8', '39761F65-1FE2-493A-B32A-20E08DF9A22F', '4901CB3B-8158-4CBB-80EB-305D68154B22', '41479DCB-7F70-42B1-8954-8214809ADB56', '2017-08-17 11:50:14');
INSERT INTO `sys_rolemenubutton` VALUES ('f0b01099-958e-4969-9f35-faa469b57686', '39761F65-1FE2-493A-B32A-20E08DF9A22F', '4901CB3B-8158-4CBB-80EB-305D68154B22', '41479DCB-7F70-42B1-8954-8214809ADB56', '2017-08-17 11:50:14');

-- ----------------------------
-- Table structure for sys_user
-- ----------------------------
DROP TABLE IF EXISTS `sys_user`;
CREATE TABLE `sys_user` (
  `KeyId` varchar(36) NOT NULL,
  `Account` varchar(20) DEFAULT NULL,
  `PassWord` varchar(50) DEFAULT NULL,
  `FullName` varchar(20) DEFAULT NULL,
  `Job` varchar(50) DEFAULT NULL,
  `Educational` varchar(20) DEFAULT NULL,
  `FinishSchool` varchar(50) DEFAULT NULL,
  `OrgId` varchar(50) DEFAULT NULL,
  `HeadImg` varchar(100) DEFAULT NULL,
  `Phone` varchar(20) DEFAULT NULL,
  `Email` varchar(50) DEFAULT NULL,
  `Sex` tinyint(4) DEFAULT NULL,
  `BirthDay` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `IDCard` varchar(18) DEFAULT NULL,
  `Address` varchar(100) DEFAULT NULL,
  `Description` varchar(150) DEFAULT NULL,
  `SortNum` int(11) DEFAULT NULL,
  `IsDeleted` tinyint(4) DEFAULT NULL,
  `CreateDate` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  PRIMARY KEY (`KeyId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of sys_user
-- ----------------------------
INSERT INTO `sys_user` VALUES ('48435A5D-9C92-4E66-96DF-CABD1E54E4D6', 'admin', 'e10adc3949ba59abbe56e057f20f883e', '开发者001', '董事长', '博士', null, 'AEE0CCBD-B8C1-45B7-B68F-B4E43A0D4964', 'http://localhost:8087/HeadImg/ad4a8033-9858-4ded-9cca-92c350541701.jpg', '11000000000', 'admin@126.com', '1', '2017-10-19 00:00:00', null, null, null, null, '0', '2017-10-19 17:08:48');
INSERT INTO `sys_user` VALUES ('5497582b-58ed-4113-b043-2d8ade3d5855', 'zhangsan', 'e10adc3949ba59abbe56e057f20f883e', '张三', '普通员工', '小学', '老年大学', '7D1EAB93-A6A3-43FD-8F23-F6B06EE78BFC', 'http://localhost:8087/HeadImg/9d1c9023-f1b2-4851-b770-2ea3de4f3058.png', '11000000000', 'zhansan@126.com', '1', '2017-10-19 00:00:00', '500101200802126666', '中国', '一个测试号', '2', '0', '2017-08-18 16:48:20');

-- ----------------------------
-- Table structure for sys_userrole
-- ----------------------------
DROP TABLE IF EXISTS `sys_userrole`;
CREATE TABLE `sys_userrole` (
  `KeyId` varchar(36) NOT NULL,
  `UserId` varchar(36) DEFAULT NULL,
  `RoleId` varchar(36) DEFAULT NULL,
  `CreateDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`KeyId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of sys_userrole
-- ----------------------------
INSERT INTO `sys_userrole` VALUES ('2ce0e256-8c7e-4fa8-b390-2439ea632b05', '48435A5D-9C92-4E66-96DF-CABD1E54E4D6', '39761F65-1FE2-493A-B32A-20E08DF9A22F', '2017-08-18 18:23:17');
INSERT INTO `sys_userrole` VALUES ('d04edb0d-446f-4b54-a491-36812b419e72', '5497582b-58ed-4113-b043-2d8ade3d5855', '1692398f-395a-4e70-9509-d78685f3d71c', '2017-10-23 16:38:24');