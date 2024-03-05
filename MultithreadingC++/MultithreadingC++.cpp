#include <windows.h>
#include <iostream>
//#include <vector>


using namespace std;

#define MAX_THREADS_NUMBER 2

//float func(float x) {
//    return x;
//}
//
//float calc_integral(float (*funcptr)(float), float start, float end) {
//    float x = start;
//    //float h = (float) 0.01;
//    float h = (float) 1;
//
//    float integral = 0;
//    while (x <= end) {
//        float curr_val = (*funcptr)(x) * h;
//        //cout << curr_val << endl;
//        integral += curr_val;
//
//        x += h;
//    }
//    return integral;
//}

int mainnn()
{
    setlocale(LC_ALL, "Russian");

    //float (*funcptr)(float) = NULL;
    //funcptr = &func;
    //float result = (*funcptr)(5);
    //cout << result << endl;

    /*float integral = calc_integral(&func, 1, 3);
    cout << "Посчитанный интеграл: " << integral << endl;*/

    cout << "REsult " << MAX_THREADS_NUMBER << endl;

    HANDLE threads_handles[MAX_THREADS_NUMBER];
    //CreateThread()



    return 0;

}
