VAR acquired_arm = false
VAR acquired_leg = false
VAR stern_door_open = false
VAR acquired_tool = false
VAR acquired_service_kit = false
VAR ship_is_sinking = false
VAR stern_visited = false
VAR funnel_visited = false
VAR superstructure_out_visited = false
VAR superstructure_in_1_visited = false
VAR superstructure_in_2_visited = false
VAR generator_room_visited = false
VAR power_restored = false
VAR bridge_visited = false
VAR talked_to_captain = false
VAR has_clearance = false
VAR talked_to_security_robot = false
VAR turned_on_lights = false
VAR memory_restored = false
VAR has_access_code = false
VAR stern_data_pad_read = false
 
== stern_load ==
>> setscene Stern
{
    - !stern_visited:
        >> sfx led_boot false
        >> fadein 4
        >> settrapped true
        Led Left "@stern_load_1"
        VacuumRobot Right "@stern_load_2"
        Led Left "@stern_load_3"
        VacuumRobot Right "@stern_load_4"
        Led Left "@stern_load_5"
        ~ stern_visited = true
        >> quest RecoverLeg Active
        >> quest RecoverArm Active
        >> quest RecoverMemory Active
        -> DONE
    - !ship_is_sinking && acquired_service_kit:
        >> screenshake true
        Led "@stern_load_6"
        >> screenshake false
        Led "@stern_load_7"
        ~ ship_is_sinking = true
        -> DONE
    - else: 
        -> DONE
}

== stern_garbage_bin ==
>> bgchange trash_on_ground
VacuumRobot "@stern_garbage_bin"
>> moveto VacuumRobot TrashBin
-> DONE

== stern_vacuum_robot ==
{
    - !acquired_leg:
        Led "@stern_vacuum_robot_0"
        >> sfx puzzle_complete true
        Led "@stern_vacuum_robot_1"
        VacuumRobot "@stern_vacuum_robot_2"
        ~ acquired_leg = true
        >> settrapped false
        >> quest RecoverLeg Completed
        -> DONE
    - else:
        VacuumRobot "@stern_vacuum_robot_3"
        Led "@stern_vacuum_robot_4"
        VacuumRobot "@stern_vacuum_robot_5"
        Led "@stern_vacuum_robot_6"
        VacuumRobot "@stern_vacuum_robot_7"
        Led "@stern_vacuum_robot_8"
        VacuumRobot "@stern_vacuum_robot_9"
        Led "@stern_vacuum_robot_10"
        VacuumRobot "@stern_vacuum_robot_11"
        Led "@stern_vacuum_robot_12"
        Led "@stern_vacuum_robot_13"
        VacuumRobot "@stern_vacuum_robot_14"
        Led "@stern_vacuum_robot_15"
        VacuumRobot "@stern_vacuum_robot_16"
        Led "@stern_vacuum_robot_17"
        VacuumRobot "@stern_vacuum_robot_18"
        Led "@stern_vacuum_robot_19"
        -> vacuum_robot_ask_table
}

== vacuum_robot_ask_table ==
    * [@vacuum_robot_ask_table_1] 
        -> ask_what_happened
    * [@vacuum_robot_ask_table_2] 
        -> why_are_we_damaged
    * [@vacuum_robot_ask_table_3]
        -> where_are_the_humans
    + [@vacuum_robot_ask_table_4] 
        -> end_discussion_with_vacuum_robot

== ask_what_happened ==
Led "@ask_what_happened_1"
VacuumRobot "@ask_what_happened_2"
Led "@ask_what_happened_3"
VacuumRobot "@ask_what_happened_4"
Led "@ask_what_happened_5"
-> vacuum_robot_ask_table

== why_are_we_damaged ==
Led "@why_are_we_damaged_1"
VacuumRobot "@why_are_we_damaged_2"
Led "@why_are_we_damaged_3"
VacuumRobot "@why_are_we_damaged_4"
Led "@why_are_we_damaged_5"
-> vacuum_robot_ask_table

== where_are_the_humans ==
Led "@where_are_the_humans_1"
VacuumRobot "@where_are_the_humans_2"
Led "@where_are_the_humans_3"
-> vacuum_robot_ask_table

== end_discussion_with_vacuum_robot ==
Led "@end_discussion_with_vacuum_robot_1"
VacuumRobot "@end_discussion_with_vacuum_robot_2"
Led "@end_discussion_with_vacuum_robot_3"
VacuumRobot "@end_discussion_with_vacuum_robot_4"
Led "@end_discussion_with_vacuum_robot_5"
VacuumRobot "@end_discussion_with_vacuum_robot_6"
VacuumRobot "@end_discussion_with_vacuum_robot_6"
VacuumRobot "@end_discussion_with_vacuum_robot_7"
Led "@end_discussion_with_vacuum_robot_8"
VacuumRobot "@end_discussion_with_vacuum_robot_9"
Led "@end_discussion_with_vacuum_robot_10"
VacuumRobot "@end_discussion_with_vacuum_robot_11"
Led "@end_discussion_with_vacuum_robot_12"
Led "@end_discussion_with_vacuum_robot_13"
-> DONE

== stern_data_pad ==
{
    - !stern_data_pad_read:
        Led "@stern_data_pad_1"
        >> showdatapad true
        >> dim 2
        >> message "@stern_data_pad_2"
        >> message "@stern_data_pad_3"
        >> message "@stern_data_pad_4"
        >> undim 2
        >> showdatapad false
        Led "@stern_data_pad_5"
        Led "@stern_data_pad_6"
        ~ stern_data_pad_read = true
    - else:
        Led "@stern_data_pad_7"
}
-> DONE

== stern_reflection ==
Led "@stern_reflection_1"
Led "@stern_reflection_2"
Led "@stern_reflection_3"
Led "@stern_reflection_4"
-> DONE

== stern_service_bot ==
Led "@stern_service_bot_1"
Led "@stern_service_bot_2"
Led "@stern_service_bot_3"
Led "@stern_service_bot_4"
Led "@stern_service_bot_5"
-> DONE

== all_in_one_tool ==
Led "@all_in_one_tool_1"
Led "@all_in_one_tool_2"
Led "@all_in_one_tool_3"
>> additem AllInOneTool
~ acquired_tool = true
-> DONE

== arm ==
>> fadeout 1
>> changesprite Led led_with_arm
>> fadein 1
>> sfx puzzle_complete true
Led "@arm_1"
    ~ acquired_arm = true
Led "@arm_2"
Led "@arm_3"
>> quest RecoverArm Completed
-> DONE

== stern_horizon ==
{
    - !ship_is_sinking:
        Led "@stern_horizon_1"
        Led "@stern_horizon_2"
        Led "@stern_horizon_3"
        {
            - acquired_arm:
                Led "@stern_horizon_4"
        }
        Led "@stern_horizon_5"
        Led "@stern_horizon_6"
        -> DONE
    - else:
        Led "@stern_horizon_7"
        Led "@stern_horizon_8"
        -> DONE
}


== stern_door ==
Led "@stern_door_1"
Led "@stern_door_2"
>> quest OpenSternDoor Active
-> DONE

== stern_pannel ==
{
    - !acquired_tool:
        Led "@stern_pannel_1"
    - !stern_door_open && acquired_tool:
        Led "@stern_pannel_2"
        {
            - acquired_arm:
                Led "@stern_pannel_3"

            - else:
                Led "@stern_pannel_4"
        }
}
-> DONE

== stern_fix_pannel ==
{
    - !acquired_arm:
        Led "@stern_fix_pannel_1"
        -> DONE
}
Led "@stern_fix_pannel_2"
Led "@stern_fix_pannel_3"
Led "@stern_fix_pannel_4"
>> removeitem AllInOneTool
>> bgchange stern_door_open
>> sfx puzzle_complete true
Led "@stern_fix_pannel_5"
Led "@stern_fix_pannel_6"
Led "@stern_fix_pannel_7"
Led "@stern_fix_pannel_8"
Led "@stern_fix_pannel_9"
Led "@stern_fix_pannel_10"
~ stern_door_open = true
>> quest OpenSternDoor Completed
-> DONE

== stern_dead_cleaning_robot ==
Led "@stern_dead_cleaning_robot_1"
Led "@stern_dead_cleaning_robot_2"
Led "@stern_dead_cleaning_robot_3"
Led "@stern_dead_cleaning_robot_4"
{
    - !acquired_arm:
        Led "@stern_dead_cleaning_robot_5"
        Led "@stern_dead_cleaning_robot_6"
}
-> DONE

== recover_memory ==
Led "@recover_memory_1"
Led "@recover_memory_2"
Led "@recover_memory_3"
Led "@recover_memory_4"
Led "@recover_memory_5"
>> fadeout 1
>> alert "@recover_memory_patch"
>> sfx puzzle_complete true
>> fadein 1
Led "@recover_memory_6"
Led "@recover_memory_7"
Led "@recover_memory_8"
Led "@recover_memory_9"
Led "@recover_memory_10"
Led "@recover_memory_11"
Led "@recover_memory_12"
Led "@recover_memory_13"
Led "@recover_memory_14"
Led "@recover_memory_15"
Led "@recover_memory_16"
Led "@recover_memory_17"
Led "@recover_memory_18"
Led "@recover_memory_19"
Led "@recover_memory_20"
Led "@recover_memory_21"
Led "@recover_memory_22"
Led "@recover_memory_23"
Led "@recover_memory_24"
Led "@recover_memory_25"
Led "@recover_memory_26"
>> quest RecoverMemory Completed
>> quest FindMaster Active
~ memory_restored = true
-> DONE

== item_use_error ==
Led "!item_use_error"
-> DONE

== funnel_load ==
>> setscene Funnel
{
    - !funnel_visited:
        Led "@funnel_load_1"
        ~ funnel_visited = true
}
-> DONE

== funnel_worktable ==
Led "@funnel_worktable_1"
{
    - !acquired_service_kit:
        Led "@funnel_worktable_2"
        >> additem ServiceKit
        ~ acquired_service_kit = true
}
-> DONE

== funnel_letter ==
Led "@funnel_letter_1"
>> dim 2
>> message "@funnel_letter_2"
>> message "@funnel_letter_3"
>> message "@funnel_letter_4"
>> message "@funnel_letter_5"
>> message "@funnel_letter_6"
>> undim 2
-> DONE

== funnel_bones ==
Led "@funnel_bones_1"
Led "@funnel_bones_2"
Led "@funnel_bones_3"
Led "@funnel_bones_4"
Led "@funnel_bones_5"
-> DONE

== funnel_data_pad ==
Led "@funnel_data_pad_1"
>> showdatapad true
>> dim 2
>> message "@funnel_data_pad_2"
Led "@funnel_data_pad_3"
>> message "@funnel_data_pad_4"
>> undim 2
>> showdatapad false
-> DONE

== funnel_clothes ==
Led "@funnel_clothes_1"
-> DONE

== funnel_leave_without_memory ==
Led "@funnel_leave_without_memory_1"
-> DONE

== superstructure_out_load ==
>> setscene Superstructure_out
{
    - !superstructure_out_visited:
        Led "@superstructure_out_load_1"
        Led "@superstructure_out_load_2"
        ~ superstructure_out_visited = true
}
-> DONE

== superstructure_left_door ==
Led "@superstructure_left_door_1"
Led "@superstructure_left_door_2"
>> fadeout 1
>> fadein 1
Led "@superstructure_left_door_3"
Led "@superstructure_left_door_4"
Led "@superstructure_left_door_5"
Led "@superstructure_left_door_6"
Led "@superstructure_left_door_7"
-> DONE

== superstructure_out_empty_lifeboat ==
Led "@superstructure_out_empty_lifeboat_1"
Led "@superstructure_out_empty_lifeboat_2"
Led "@superstructure_out_empty_lifeboat_3"
-> DONE

== superstructure_out_remaining_lifeboat ==
Led Left "@superstructure_out_remaining_lifeboat_1"
-> superstructure_out_security_bot

== superstructure_out_security_bot == 
{
    - !has_clearance:
    {
        - !talked_to_security_robot:
            SecurityBot Right "@superstructure_out_security_bot_1"
            Led Left "@superstructure_out_security_bot_2"
            SecurityBot "@superstructure_out_security_bot_3"
            Led "@superstructure_out_security_bot_4"
            SecurityBot "@superstructure_out_security_bot_5"
            Led "@superstructure_out_security_bot_6"
            SecurityBot "@superstructure_out_security_bot_7"
            Led "@superstructure_out_security_bot_8"
            SecurityBot "@superstructure_out_security_bot_9"
            Led "@superstructure_out_security_bot_10"
            SecurityBot "@superstructure_out_security_bot_11"
            Led "@superstructure_out_security_bot_12"
            SecurityBot "@superstructure_out_security_bot_13"
            Led "@superstructure_out_security_bot_14"
            SecurityBot "@superstructure_out_security_bot_15"
            Led "@superstructure_out_security_bot_16"
            SecurityBot "@superstructure_out_security_bot_17"
            Led "@superstructure_out_security_bot_18"
            SecurityBot "@superstructure_out_security_bot_19"
            Led "@superstructure_out_security_bot_20"
            SecurityBot "@superstructure_out_security_bot_21"
            Led "@superstructure_out_security_bot_22"
            SecurityBot "@superstructure_out_security_bot_23"
            Led "@superstructure_out_security_bot_24"
            SecurityBot "@superstructure_out_security_bot_25"
            Led "@superstructure_out_security_bot_26"
            Led "@superstructure_out_security_bot_27"
            Led "@superstructure_out_security_bot_28"
            Led "@superstructure_out_security_bot_29"
            SecurityBot "@superstructure_out_security_bot_30"
            Led "@superstructure_out_security_bot_31"
            SecurityBot "@superstructure_out_security_bot_32"
            Led "@superstructure_out_security_bot_33"
            SecurityBot "@superstructure_out_security_bot_34"
            Led "@superstructure_out_security_bot_35"
            SecurityBot "@superstructure_out_security_bot_36"
            Led "@superstructure_out_security_bot_37"
            SecurityBot "@superstructure_out_security_bot_38"
            Led "@superstructure_out_security_bot_39"
            SecurityBot "@superstructure_out_security_bot_40"
            Led "@superstructure_out_security_bot_41"
            Led "@superstructure_out_security_bot_42"
            >> quest GetClearance Active
            ~ talked_to_security_robot = true
        - else:
            Led "@superstructure_out_security_bot_42"
    }
    - else:
        Led Left "@superstructure_out_security_bot_43"
        SecurityBot Right "@superstructure_out_security_bot_44"
        Led "@superstructure_out_security_bot_45"
        SecurityBot "@superstructure_out_security_bot_46"
        Led "@superstructure_out_security_bot_47"
        SecurityBot "@superstructure_out_security_bot_48"
        Led "@superstructure_out_security_bot_49"
        SecurityBot "@superstructure_out_security_bot_50"
        Led "@superstructure_out_security_bot_51"
        >> quest GetClearance Completed
}

-> DONE

== superstructure_in_load ==
{
    - !superstructure_in_1_visited:
        Led "@superstructure_in_load_1"
        Led "@superstructure_in_load_2"
        Led "@superstructure_in_load_3"
        Led "@superstructure_in_load_4"
        Led "@superstructure_in_load_5"
        Led "@superstructure_in_load_6"
        Led "@superstructure_in_load_7"
        ~ superstructure_in_1_visited = true
    - !superstructure_in_2_visited && power_restored:
        Led "@superstructure_in_load_8"
        Led "@superstructure_in_load_9"
        Led "@superstructure_in_load_10"
        Led "@superstructure_in_load_11"
        Led "@superstructure_in_load_12"
        ~ superstructure_in_2_visited = true
}
-> DONE

== superstructure_in_door ==
>> sfx negative_beep true
Led "@superstructure_in_door_1"
-> DONE

== superstructure_in_power_grid ==
Led "@superstructure_in_power_grid_1"
Led "@superstructure_in_power_grid_2"
Led "@superstructure_in_power_grid_3"
Led "@superstructure_in_power_grid_4"
Led "@superstructure_in_power_grid_5"
Led "@superstructure_in_power_grid_6"
Led "@superstructure_in_power_grid_7"
Led "@superstructure_in_power_grid_8"
Led "@superstructure_in_power_grid_9"
Led "@superstructure_in_power_grid_10"
Led "@superstructure_in_power_grid_11"
-> DONE

== superstructure_in_data_pad ==
Led "@superstructure_in_data_pad_1"
Led "@superstructure_in_data_pad_2"
Led "@superstructure_in_data_pad_3"
Led "@superstructure_in_data_pad_4"
Led "@superstructure_in_data_pad_5"
Led "@superstructure_in_data_pad_6"
Led "@superstructure_in_data_pad_7"
Led "@superstructure_in_data_pad_8"
Led "@superstructure_in_data_pad_9"
Led "@superstructure_in_data_pad_10"
-> DONE

== superstructure_in_dead_bot ==
Led "@superstructure_in_dead_bot_1"
Led "@superstructure_in_dead_bot_2"
Led "@superstructure_in_dead_bot_3"
Led "@superstructure_in_dead_bot_4"
Led "@superstructure_in_dead_bot_5"
Led "@superstructure_in_dead_bot_6"
Led "@superstructure_in_dead_bot_7"
Led "@superstructure_in_dead_bot_8"
Led "@superstructure_in_dead_bot_9"
Led "@superstructure_in_dead_bot_10"
Led "@superstructure_in_dead_bot_11"
Led "@superstructure_in_dead_bot_12"
-> DONE

== superstructure_in_blood ==
Led "@superstructure_in_blood_1"
Led "@superstructure_in_blood_2"
Led "@superstructure_in_blood_3"
Led "@superstructure_in_blood_4"
Led "@superstructure_in_blood_5"
Led "@superstructure_in_blood_6"
Led "@superstructure_in_blood_7"
Led "@superstructure_in_blood_8"
Led "@superstructure_in_blood_9"
Led "@superstructure_in_blood_10"
-> DONE

== superstructure_in_corpse ==
Led "@superstructure_in_corpse_1"
Led "@superstructure_in_corpse_2"
Led "@superstructure_in_corpse_3"
Led "@superstructure_in_corpse_4"
Led "@superstructure_in_corpse_5"
Led "@superstructure_in_corpse_6"
~ has_access_code = true
-> DONE

== superstructure_in_monitor ==
Led "@superstructure_in_monitor_1"
-> DONE

== superstructure_in_map ==
Led "@superstructure_in_map_1"
Led "@superstructure_in_map_2"
Led "@superstructure_in_map_3"
-> DONE

== superstructure_in_leave_without_power ==
Led "@superstructure_in_leave_without_power_1"
-> DONE

== superstructure_in_no_access_code ==
Led "@superstructure_in_no_access_code_1"
-> DONE

== generator_room_load ==
{
    - !generator_room_visited:
        Led "@generator_room_load_1"
        ~ generator_room_visited = true
}
-> DONE

== generator_room_board ==
Led "@generator_room_board_1"
Led "@generator_room_board_2"
>> bgchange generator_room_lit
~ turned_on_lights = true
-> DONE

== generator_room_computer ==
{
    - !turned_on_lights:
        Led "@computer_no_lights"
        -> DONE
    - else:
        Led "@generator_room_computer_1"
        Led "@generator_room_computer_2"
        Led "@generator_room_computer_3"
        Led "@generator_room_computer_4"
        Led "@generator_room_computer_5"
        -> generator_room_log_table
}


== generator_room_log_table ==
    + [@generator_room_log_title_1]
        Unknown "@generator_room_log_1.1"
        Unknown "@generator_room_log_1.2"
        Unknown "@generator_room_log_1.3"
        Unknown "@generator_room_log_1.4"
        -> generator_room_log_table
    + [@generator_room_log_title_2]
        Unknown "@generator_room_log_2.1"
        Unknown "@generator_room_log_2.2"
        Unknown "@generator_room_log_2.3"
        Unknown "@generator_room_log_2.4"
        Unknown "@generator_room_log_2.5"
        -> generator_room_log_table
    + [@generator_room_log_title_3]
        Unknown "@generator_room_log_3.1"
        Unknown "@generator_room_log_3.2"
        Unknown "@generator_room_log_3.3"
        -> generator_room_log_table
    + [@generator_room_log_title_4]
        Unknown "@generator_room_log_4.1"
        Unknown "@generator_room_log_4.2"
        Unknown "@generator_room_log_4.3"
        Unknown "@generator_room_log_4.4"
        Unknown "@generator_room_log_4.5"
        -> generator_room_log_table
    + [@generator_room_log_title_5]
        Unknown "@generator_room_log_5.1"
        Unknown "@generator_room_log_5.2"
        Unknown "@generator_room_log_5.3"
        Unknown "@generator_room_log_5.4"
        -> generator_room_log_table
    + [@generator_room_log_title_6]
        Unknown "@generator_room_log_6.1"
        Unknown "@generator_room_log_6.2"
        Unknown "@generator_room_log_6.3"
        Unknown "@generator_room_log_6.4"
        -> generator_room_log_table
    + [@generator_room_log_title_7]
        Unknown "@generator_room_log_7.1"
        Unknown "@generator_room_log_7.2"
        Unknown "@generator_room_log_7.3"
        Unknown "@generator_room_log_7.4"
        Unknown "@generator_room_log_7.5"
        Unknown "@generator_room_log_7.6"
        -> generator_room_log_table
    + [@generator_room_log_title_return]
        Led "@generator_room_log_return"
        -> DONE

== bridge_load ==
>> screenshake true
Led "@bridge_load_1"
>> screenshake false
Led "@bridge_load_2"
Led "@bridge_load_3"
Led "@bridge_load_4"
Led "@bridge_load_5"
Led "@bridge_load_6"
>> alert "@bridge_load_7"
>> alert "@bridge_load_8"
Led "@bridge_load_9"
Led "@bridge_load_10"
Led "@bridge_load_11"
Led "@bridge_load_12"
Led "@bridge_load_13"
>> alert "@bridge_load_14"
Led "@bridge_load_15"
Led "@bridge_load_16"
>> alert "@bridge_load_17"
Led "@bridge_load_18"
-> DONE

== bridge_captain ==
{
    - !talked_to_captain:
        CaptainBot Right "@bridge_captain_1"
        Led Left "@bridge_captain_2"
        CaptainBot Right "@bridge_captain_3"
        Led Left "@bridge_captain_4"
        CaptainBot Right "@bridge_captain_5"
        Led Left "@bridge_captain_6"
        CaptainBot Right "@bridge_captain_7"
        Led Left "@bridge_captain_8"
        CaptainBot Right "@bridge_captain_9"
        CaptainBot Right "@bridge_captain_10"
        Led Left "@bridge_captain_11"
        CaptainBot Right "@bridge_captain_12"
        Led Left "@bridge_captain_13"
        CaptainBot Right "@bridge_captain_14"
        CaptainBot Right "@bridge_captain_15"
        Led Left "@bridge_captain_16"
        CaptainBot Right "@bridge_captain_17"
        CaptainBot Right "@bridge_captain_18"
        CaptainBot Right "@bridge_captain_19"
        Led Left "@bridge_captain_20"
        CaptainBot Right "@bridge_captain_21"
        CaptainBot Right "@bridge_captain_22"
        Led Left "@bridge_captain_23"
        CaptainBot Right "@bridge_captain_24"
        CaptainBot Right "@bridge_captain_25"
        CaptainBot Right "@bridge_captain_26"
        Led Left "@bridge_captain_27"
        CaptainBot Right "@bridge_captain_28"
        Led Left "@bridge_captain_29"
        CaptainBot Right "@bridge_captain_30"
        Led Left "@bridge_captain_31"
        CaptainBot Right "@bridge_captain_32"
        Led Left "@bridge_captain_33"
        CaptainBot Right "@bridge_captain_34"
        Led Left "@bridge_captain_35"
        CaptainBot Right "@bridge_captain_36"
        Led Left "@bridge_captain_37"
        CaptainBot Right "@bridge_captain_38"
        Led Left "@bridge_captain_39"
        CaptainBot Right "@bridge_captain_40"
        Led Left "@bridge_captain_41"
        CaptainBot Right "@bridge_captain_42"
        ~ talked_to_captain = true
    - else:
        Led Left "@bridge_captain_43"
        CaptainBot Right "@bridge_captain_44"
        Led Left "@bridge_captain_45"
        CaptainBot Right "@bridge_captain_46"
        Led Left "@bridge_captain_47"
        CaptainBot Right "@bridge_captain_48"
        Led Left "@bridge_captain_49"
        CaptainBot Right "@bridge_captain_50"
        CaptainBot Right "@bridge_captain_51"
        CaptainBot Right "@bridge_captain_52"
        ~ has_clearance = true
        >> additem ClearanceCard
        >> quest GetClearance Completed
}

-> DONE

== bridge_captain_hat ==
Led Left "@bridge_captain_hat_1"
CaptainBot Right "@bridge_captain_hat_2"
CaptainBot Right "@bridge_captain_hat_3"
-> DONE

== bridge_corpse_a ==
Led "@bridge_corpse_a_1"
Led "@bridge_corpse_a_2"
-> DONE

== bridge_corpse_b ==
Led "@bridge_corpse_b_1"
Led "@bridge_corpse_b_2"
-> DONE

== bridge_data_pad ==
Led "@bridge_data_pad_1"
Led "@bridge_data_pad_2"
Unknown "@bridge_data_pad_3"
Unknown "@bridge_data_pad_4"
Led "@bridge_data_pad_5"
Led "@bridge_data_pad_6"
-> DONE