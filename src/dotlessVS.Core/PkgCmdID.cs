/* Copyright 2009 dotless project, http://www.dotlesscss.com
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 *     
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License. */

using System;

namespace LessProject.DotLessIntegration
{
    static class PkgCmdIDList
    {



        // Menus
        public const int IDM_TLB_RTF = 0x0001;			// toolbar
        public const int IDMX_RTF = 0x0002;			// context menu
        public const int IDM_RTFMNU_ALIGN = 0x0004;
        public const int IDM_RTFMNU_SIZE = 0x0005;

        // Menu Groups
        public const int IDG_RTF_FMT_FONT1 = 0x1000;
        public const int IDG_RTF_FMT_FONT2 = 0x1001;
        public const int IDG_RTF_FMT_INDENT = 0x1002;
        public const int IDG_RTF_FMT_BULLET = 0x1003;

        public const int IDG_RTF_TLB_FONT1 = 0x1004;
        public const int IDG_RTF_TLB_FONT2 = 0x1005;
        public const int IDG_RTF_TLB_INDENT = 0x1006;
        public const int IDG_RTF_TLB_BULLET = 0x1007;
        public const int IDG_RTF_TLB_FONT_COMBOS = 0x1008;

        public const int IDG_RTF_CTX_EDIT = 0x1009;
        public const int IDG_RTF_CTX_PROPS = 0x100a;

        public const int IDG_RTF_EDITOR_CMDS = 0x100b;

        // Command IDs

        public const int icmdStrike = 0x0004;

    };
}