﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;

namespace SortCommon
{
    public partial class HalconHelper
    {
        private static HTuple hv_ExpDefaultWinHandle;

        public event EventHandler<OnCompleteEventArgs> Complete;
        public event EventHandler<OnErrorEventArgs> Error;

        public bool CameraConnect { get; set; }
        public bool MotorConnect { get; set; }
        public bool WeighterConnect { get; set; }
        

        public HalconHelper(HTuple Window)
        {
            HOperatorSet.SetSystem("do_low_error", "false");
            hv_ExpDefaultWinHandle = Window;
        }
        // Chapter: Graphics / Text
        // Short Description: This procedure writes a text message. 
        public static void disp_message(HTuple hv_WindowHandle, HTuple hv_String, HTuple hv_CoordSystem,
            HTuple hv_Row, HTuple hv_Column, HTuple hv_Color, HTuple hv_Box)
        {

            if (hv_ExpDefaultWinHandle == null)
                return;

            // Local iconic variables 

                // Local control variables 

            HTuple hv_Red = null, hv_Green = null, hv_Blue = null;
            HTuple hv_Row1Part = null, hv_Column1Part = null, hv_Row2Part = null;
            HTuple hv_Column2Part = null, hv_RowWin = null, hv_ColumnWin = null;
            HTuple hv_WidthWin = null, hv_HeightWin = null, hv_MaxAscent = null;
            HTuple hv_MaxDescent = null, hv_MaxWidth = null, hv_MaxHeight = null;
            HTuple hv_R1 = new HTuple(), hv_C1 = new HTuple(), hv_FactorRow = new HTuple();
            HTuple hv_FactorColumn = new HTuple(), hv_UseShadow = null;
            HTuple hv_ShadowColor = null, hv_Exception = new HTuple();
            HTuple hv_Width = new HTuple(), hv_Index = new HTuple();
            HTuple hv_Ascent = new HTuple(), hv_Descent = new HTuple();
            HTuple hv_W = new HTuple(), hv_H = new HTuple(), hv_FrameHeight = new HTuple();
            HTuple hv_FrameWidth = new HTuple(), hv_R2 = new HTuple();
            HTuple hv_C2 = new HTuple(), hv_DrawMode = new HTuple();
            HTuple hv_CurrentColor = new HTuple();
            HTuple hv_Box_COPY_INP_TMP = hv_Box.Clone();
            HTuple hv_Color_COPY_INP_TMP = hv_Color.Clone();
            HTuple hv_Column_COPY_INP_TMP = hv_Column.Clone();
            HTuple hv_Row_COPY_INP_TMP = hv_Row.Clone();
            HTuple hv_String_COPY_INP_TMP = hv_String.Clone();

            // Initialize local and output iconic variables 
            //This procedure displays text in a graphics window.
            //
            //Input parameters:
            //WindowHandle: The WindowHandle of the graphics window, where
            //   the message should be displayed
            //String: A tuple of strings containing the text message to be displayed
            //CoordSystem: If set to 'window', the text position is given
            //   with respect to the window coordinate system.
            //   If set to 'image', image coordinates are used.
            //   (This may be useful in zoomed images.)
            //Row: The row coordinate of the desired text position
            //   If set to -1, a default value of 12 is used.
            //Column: The column coordinate of the desired text position
            //   If set to -1, a default value of 12 is used.
            //Color: defines the color of the text as string.
            //   If set to [], '' or 'auto' the currently set color is used.
            //   If a tuple of strings is passed, the colors are used cyclically
            //   for each new textline.
            //Box: If Box[0] is set to 'true', the text is written within an orange box.
            //     If set to' false', no box is displayed.
            //     If set to a color string (e.g. 'white', '#FF00CC', etc.),
            //       the text is written in a box of that color.
            //     An optional second value for Box (Box[1]) controls if a shadow is displayed:
            //       'true' -> display a shadow in a default color
            //       'false' -> display no shadow (same as if no second value is given)
            //       otherwise -> use given string as color string for the shadow color
            //
            //Prepare window
            hv_WindowHandle = hv_ExpDefaultWinHandle;
            HOperatorSet.GetRgb(hv_WindowHandle, out hv_Red, out hv_Green, out hv_Blue);
            HOperatorSet.GetPart(hv_WindowHandle, out hv_Row1Part, out hv_Column1Part, out hv_Row2Part,
                out hv_Column2Part);
            HOperatorSet.GetWindowExtents(hv_WindowHandle, out hv_RowWin, out hv_ColumnWin,
                out hv_WidthWin, out hv_HeightWin);
            HOperatorSet.SetPart(hv_WindowHandle, 0, 0, hv_HeightWin - 1, hv_WidthWin - 1);
            //
            //default settings
            if ((int)(new HTuple(hv_Row_COPY_INP_TMP.TupleEqual(-1))) != 0)
            {
                hv_Row_COPY_INP_TMP = 12;
            }
            if ((int)(new HTuple(hv_Column_COPY_INP_TMP.TupleEqual(-1))) != 0)
            {
                hv_Column_COPY_INP_TMP = 12;
            }
            if ((int)(new HTuple(hv_Color_COPY_INP_TMP.TupleEqual(new HTuple()))) != 0)
            {
                hv_Color_COPY_INP_TMP = "";
            }
            //
            hv_String_COPY_INP_TMP = ((("" + hv_String_COPY_INP_TMP) + "")).TupleSplit("\n");
            //
            //Estimate extentions of text depending on font size.
            HOperatorSet.GetFontExtents(hv_WindowHandle, out hv_MaxAscent, out hv_MaxDescent,
                out hv_MaxWidth, out hv_MaxHeight);
            if ((int)(new HTuple(hv_CoordSystem.TupleEqual("window"))) != 0)
            {
                hv_R1 = hv_Row_COPY_INP_TMP.Clone();
                hv_C1 = hv_Column_COPY_INP_TMP.Clone();
            }
            else
            {
                //Transform image to window coordinates
                hv_FactorRow = (1.0 * hv_HeightWin) / ((hv_Row2Part - hv_Row1Part) + 1);
                hv_FactorColumn = (1.0 * hv_WidthWin) / ((hv_Column2Part - hv_Column1Part) + 1);
                hv_R1 = ((hv_Row_COPY_INP_TMP - hv_Row1Part) + 0.5) * hv_FactorRow;
                hv_C1 = ((hv_Column_COPY_INP_TMP - hv_Column1Part) + 0.5) * hv_FactorColumn;
            }
            //
            //Display text box depending on text size
            hv_UseShadow = 1;
            hv_ShadowColor = "gray";
            if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(0))).TupleEqual("true"))) != 0)
            {
                if (hv_Box_COPY_INP_TMP == null)
                    hv_Box_COPY_INP_TMP = new HTuple();
                hv_Box_COPY_INP_TMP[0] = "#fce9d4";
                hv_ShadowColor = "#f28d26";
            }
            if ((int)(new HTuple((new HTuple(hv_Box_COPY_INP_TMP.TupleLength())).TupleGreater(
                1))) != 0)
            {
                if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(1))).TupleEqual("true"))) != 0)
                {
                    //Use default ShadowColor set above
                }
                else if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(1))).TupleEqual(
                    "false"))) != 0)
                {
                    hv_UseShadow = 0;
                }
                else
                {
                    hv_ShadowColor = hv_Box_COPY_INP_TMP[1];
                    //Valid color?
                    try
                    {
                        HOperatorSet.SetColor(hv_WindowHandle, hv_Box_COPY_INP_TMP.TupleSelect(
                            1));
                    }
                    // catch (Exception) 
                    catch (HalconException HDevExpDefaultException1)
                    {
                        HDevExpDefaultException1.ToHTuple(out hv_Exception);
                        hv_Exception = "Wrong value of control parameter Box[1] (must be a 'true', 'false', or a valid color string)";
                        throw new HalconException(hv_Exception);
                    }
                }
            }
            if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(0))).TupleNotEqual("false"))) != 0)
            {
                //Valid color?
                try
                {
                    HOperatorSet.SetColor(hv_WindowHandle, hv_Box_COPY_INP_TMP.TupleSelect(0));
                }
                // catch (Exception) 
                catch (HalconException HDevExpDefaultException1)
                {
                    HDevExpDefaultException1.ToHTuple(out hv_Exception);
                    hv_Exception = "Wrong value of control parameter Box[0] (must be a 'true', 'false', or a valid color string)";
                    throw new HalconException(hv_Exception);
                }
                //Calculate box extents
                hv_String_COPY_INP_TMP = (" " + hv_String_COPY_INP_TMP) + " ";
                hv_Width = new HTuple();
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                    )) - 1); hv_Index = (int)hv_Index + 1)
                {
                    HOperatorSet.GetStringExtents(hv_WindowHandle, hv_String_COPY_INP_TMP.TupleSelect(
                        hv_Index), out hv_Ascent, out hv_Descent, out hv_W, out hv_H);
                    hv_Width = hv_Width.TupleConcat(hv_W);
                }
                hv_FrameHeight = hv_MaxHeight * (new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                    ));
                hv_FrameWidth = (((new HTuple(0)).TupleConcat(hv_Width))).TupleMax();
                hv_R2 = hv_R1 + hv_FrameHeight;
                hv_C2 = hv_C1 + hv_FrameWidth;
                //Display rectangles
                HOperatorSet.GetDraw(hv_WindowHandle, out hv_DrawMode);
                HOperatorSet.SetDraw(hv_WindowHandle, "fill");
                //Set shadow color
                HOperatorSet.SetColor(hv_WindowHandle, hv_ShadowColor);
                if ((int)(hv_UseShadow) != 0)
                {
                    HOperatorSet.DispRectangle1(hv_WindowHandle, hv_R1 + 1, hv_C1 + 1, hv_R2 + 1, hv_C2 + 1);
                }
                //Set box color
                HOperatorSet.SetColor(hv_WindowHandle, hv_Box_COPY_INP_TMP.TupleSelect(0));
                HOperatorSet.DispRectangle1(hv_WindowHandle, hv_R1, hv_C1, hv_R2, hv_C2);
                HOperatorSet.SetDraw(hv_WindowHandle, hv_DrawMode);
            }
            //Write text.
            for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                )) - 1); hv_Index = (int)hv_Index + 1)
            {
                hv_CurrentColor = hv_Color_COPY_INP_TMP.TupleSelect(hv_Index % (new HTuple(hv_Color_COPY_INP_TMP.TupleLength()
                    )));
                if ((int)((new HTuple(hv_CurrentColor.TupleNotEqual(""))).TupleAnd(new HTuple(hv_CurrentColor.TupleNotEqual(
                    "auto")))) != 0)
                {
                    HOperatorSet.SetColor(hv_WindowHandle, hv_CurrentColor);
                }
                else
                {
                    HOperatorSet.SetRgb(hv_WindowHandle, hv_Red, hv_Green, hv_Blue);
                }
                hv_Row_COPY_INP_TMP = hv_R1 + (hv_MaxHeight * hv_Index);
                HOperatorSet.SetTposition(hv_WindowHandle, hv_Row_COPY_INP_TMP, hv_C1);
                HOperatorSet.WriteString(hv_WindowHandle, hv_String_COPY_INP_TMP.TupleSelect(
                    hv_Index));
            }
            //Reset changed window settings
            HOperatorSet.SetRgb(hv_WindowHandle, hv_Red, hv_Green, hv_Blue);
            HOperatorSet.SetPart(hv_WindowHandle, hv_Row1Part, hv_Column1Part, hv_Row2Part,
                hv_Column2Part);

            return;
        }

        public void BarRecognition(HObject ho_Image)
        {
            HTuple hv_BarCodeHandle = new HTuple();
            HTuple hv_MeasThreshAbsValue = new HTuple();
            HObject ho_SymbolRegions = null,ho_BarCodeObjects = null,ho_ObjectSelected, ho_ImageMirror2;
            HTuple hv_CodeTypes = new HTuple();
            HTuple hv_DecodedDataStrings = new HTuple(),hv_DecodedDataTypes = new HTuple();
            HTuple hv_Start = new HTuple(), hv_Stop = new HTuple(), hv_Duration = new HTuple();
            HTuple hv_J = new HTuple();
            string Codetype;
            int i;

            try
            {
                // Initialize local and output iconic variables  
                HOperatorSet.GenEmptyObj(out ho_ImageMirror2);
                HOperatorSet.GenEmptyObj(out ho_SymbolRegions);
                HOperatorSet.GenEmptyObj(out ho_BarCodeObjects);
                HOperatorSet.GenEmptyObj(out ho_ObjectSelected);


                HTuple Pointer, type, Width, Height;

                ho_ImageMirror2.Dispose();
                HOperatorSet.MirrorImage(ho_Image, out ho_ImageMirror2, "row");
                HOperatorSet.GetImagePointer1(ho_ImageMirror2, out Pointer, out type, out Width, out Height);
                HOperatorSet.SetPart(hv_ExpDefaultWinHandle, 0, 0, Height, Width);

                HOperatorSet.DispObj(ho_ImageMirror2, hv_ExpDefaultWinHandle);

                //hv_MeasThreshAbsValue = 10.0;
                //hv_CodeTypes = "auto";
                hv_CodeTypes[0] = "Code 128";
                hv_CodeTypes[1] = "Code 39";
                hv_CodeTypes[2] = "Code 93";
                hv_CodeTypes[3] = "EAN-13";
                HOperatorSet.SetDraw(hv_ExpDefaultWinHandle, "margin");
                HOperatorSet.SetLineWidth(hv_ExpDefaultWinHandle, 4);

                HOperatorSet.CreateBarCodeModel(new HTuple(), new HTuple(), out hv_BarCodeHandle);
                //HOperatorSet.SetBarCodeParam(hv_BarCodeHandle, "meas_thresh_abs", hv_MeasThreshAbsValue);
                HOperatorSet.SetBarCodeParam(hv_BarCodeHandle, "element_size_min", 1.0);
                HOperatorSet.SetBarCodeParam(hv_BarCodeHandle, "element_size_max", 8);
                HOperatorSet.SetBarCodeParam(hv_BarCodeHandle, "check_char", "present");
                HOperatorSet.SetBarCodeParam(hv_BarCodeHandle, "meas_param_estimation", "true");
                HOperatorSet.SetBarCodeParam(hv_BarCodeHandle, "majority_voting", "true");
                HOperatorSet.SetBarCodeParam(hv_BarCodeHandle, "element_size_variable", "true");
                HOperatorSet.SetBarCodeParam(hv_BarCodeHandle, "start_stop_tolerance", "low");
                HOperatorSet.SetBarCodeParam(hv_BarCodeHandle, "quiet_zone", "true");
                

                // HOperatorSet.SetBarCodeParam(hv_BarCodeHandle, "stop_after_result_num", 2);
                ho_SymbolRegions.Dispose();
                HOperatorSet.CountSeconds(out hv_Start);
                HOperatorSet.FindBarCode(ho_ImageMirror2, out ho_SymbolRegions, hv_BarCodeHandle, hv_CodeTypes, out hv_DecodedDataStrings);
                HOperatorSet.CountSeconds(out hv_Stop);
                hv_Duration = (hv_Stop - hv_Start) * 1000;
                ho_BarCodeObjects.Dispose();
                HOperatorSet.GetBarCodeObject(out ho_BarCodeObjects, hv_BarCodeHandle, "all", "symbol_regions");

                HOperatorSet.GetBarCodeResult(hv_BarCodeHandle, "all", "decoded_types", out hv_DecodedDataTypes);

                i = new HTuple(hv_DecodedDataStrings.TupleLength());
                if (i == 1)
                {
                    Codetype = hv_DecodedDataTypes.S;
                    HOperatorSet.SetColor(hv_ExpDefaultWinHandle, "forest green");
                    HOperatorSet.SelectObj(ho_BarCodeObjects, out ho_ObjectSelected, 1);
                    HOperatorSet.DispObj(ho_ObjectSelected, hv_ExpDefaultWinHandle);
                    if (Complete != null)
                    {
                        this.Complete(this, new OnCompleteEventArgs(hv_DecodedDataStrings.S, Codetype, hv_Duration));
                    }  

                  
                }
                else
                {
                    for (hv_J = 0; (int)hv_J <= (int)((new HTuple(hv_DecodedDataStrings.TupleLength())) - 1); hv_J = (int)hv_J + 1)
                    {
                        HOperatorSet.SetColor(hv_ExpDefaultWinHandle, "red");
                        ho_ObjectSelected.Dispose();
                        HOperatorSet.SelectObj(ho_BarCodeObjects, out ho_ObjectSelected, hv_J + 1);
                        HOperatorSet.DispObj(ho_ObjectSelected, hv_ExpDefaultWinHandle);
                    }
                    if (Error!=null)
                    {
                        this.Error(this,new OnErrorEventArgs(i));
                    }
                }
                HOperatorSet.ClearBarCodeModel(hv_BarCodeHandle);
                ho_Image.Dispose();
                ho_ImageMirror2.Dispose();
                ho_SymbolRegions.Dispose();
                ho_BarCodeObjects.Dispose();

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
            }

        }
        public static void Action(HObject ho_Image)
        {

        }

    }
    /// <summary>
    /// 一维码识别完成事件
    /// </summary>
    public class OnCompleteEventArgs
    {
        public string DataStrings { get; private set; }
        public string DataTypes { get; private set; }
        public double UseTime { get; }
        public OnCompleteEventArgs(string Datastr,string Dataty,double Time)
        {
            this.DataStrings = Datastr;
            this.DataTypes = Dataty;
            this.UseTime = Time;


        }
    }

    public class OnErrorEventArgs
    {
        public int Num { get; }
        public OnErrorEventArgs(int num)
        {
            this.Num = num;
        }
    }
}
