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
    static class GuidList
    {
        public const string guidDotLessIntegrationPkgString = "DF8C7FAD-2BF3-45ae-B78B-037B8C4A5DE8";
        public const string guidDotLessIntegrationCmdSetString = "40D57D21-E2F5-45df-9732-430397A925D1";
        public const string guidDotLessIntegrationEditorFactoryString = "BCC05326-A4D6-4821-B0D3-9ED51E1A27DD";

        public static readonly Guid guidDotLessIntegrationCmdSet = new Guid(guidDotLessIntegrationCmdSetString);
        public static readonly Guid guidDotLessIntegrationEditorFactory = new Guid(guidDotLessIntegrationEditorFactoryString);
    };
}