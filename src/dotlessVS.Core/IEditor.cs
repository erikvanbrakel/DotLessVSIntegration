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
using System.Runtime.InteropServices;
using EnvDTE;
using tom;

namespace LessProject.DotLessIntegration
{

    /// <summary>
    /// IEditor is the automation interface for EditorDocument.
    /// The implementation of the methods is just a wrapper over the rich
    /// edit control's object model.
    /// </summary>
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IEditor
    {
        float DefaultTabStop { get; set; }
        ITextRange Range { get; }
        ITextSelection Selection { get; }
        int SelectionProperties { get; set; }
        int FindText(string textToFind);
        int SetText(string textToSet);
        int TypeText(string textToType);
        int Cut();
        int Copy();
        int Paste();
        int Delete(long unit, long count);
        int MoveUp(int unit, int count, int extend);
        int MoveDown(int unit, int count, int extend);
        int MoveLeft(int unit, int count, int extend);
        int MoveRight(int unit, int count, int extend);
        int EndKey(int unit, int extend);
        int HomeKey(int unit, int extend);
    }
}
